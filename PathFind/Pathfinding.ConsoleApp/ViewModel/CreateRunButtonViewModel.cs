using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.Exceptions;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.Domain.Interface;
using System.Linq;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Extensions;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal abstract class CreateRunButtonViewModel : BaseViewModel
    {
        protected readonly IRequestService<VertexModel> service;
        protected readonly IMessenger messenger;
        protected readonly ILog logger;

        protected abstract string AlgorithmId { get;  }

        public ReactiveCommand<MouseEventArgs, Unit> CreateRunCommand { get; }

        private int activatedGraphId;
        private int ActivatedGraphId
        {
            get => activatedGraphId;
            set => this.RaiseAndSetIfChanged(ref activatedGraphId, value);
        }

        private IGraph<VertexModel> activatedGraph;
        private IGraph<VertexModel> ActivatedGraph
        {
            get => activatedGraph;
            set => this.RaiseAndSetIfChanged(ref activatedGraph, value);
        }

        private IObservable<bool> CanCreate()
        {
            return this.WhenAnyValue(
                x => x.ActivatedGraphId,
                x => x.ActivatedGraph,
                (id, graph) => id > 0 && graph != null);
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            ActivatedGraph = msg.Graph;
            ActivatedGraphId = msg.GraphId;
        }

        protected CreateRunButtonViewModel(IRequestService<VertexModel> service,
            IMessenger messenger, 
            ILog logger)
        {
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            CreateRunCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(CreateRun, CanCreate());
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
        }

        protected abstract PathfindingProcess GetAlgorithm(IEnumerable<VertexModel> pathfindingRange);

        protected abstract void AppendStatistics(RunStatisticsModel model);

        private async Task CreateRun(MouseEventArgs e)
        {
            var range = await service.ReadRangeAsync(ActivatedGraphId);

            if (range.Count > 0)
            {
                var subAlgorithms = new List<CreateSubAlgorithmRequest>();
                int visitedCount = 0;
                var visitedVertices = new List<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)>();

                void OnVertexEnqueued(object sender, VerticesEnqueuedEventArgs e)
                {
                    visitedCount++;
                    visitedVertices.Add((e.Current, e.Enqueued));
                }

                void OnSubPathFound(object sender, SubPathFoundEventArgs args)
                {
                    ModelBuilder.CreateSubAlgorithmRequest()
                        .WithOrder(subAlgorithms.Count)
                        .WithPath(args.SubPath)
                        .WithVisitedVertices(visitedVertices.ToList())
                        .AddTo(subAlgorithms);
                    visitedVertices.Clear();
                }

                string status = RunStatuses.Success;
                var stopwatch = new Stopwatch();
                var pathfindingRange = range
                    .OrderBy(x => x.Order)
                    .Select(x => x.Position)
                    .Select(ActivatedGraph.Get)
                    .ToList();
                var algorithm = GetAlgorithm(pathfindingRange);

                algorithm.SubPathFound += OnSubPathFound;
                algorithm.VertexEnqueued += OnVertexEnqueued;

                stopwatch.Start();
                var path = NullGraphPath.Interface;
                try
                {
                    path = await algorithm.FindPathAsync();
                }
                catch (PathfindingException ex)
                {
                    status = RunStatuses.Failure;
                    logger.Warn(ex);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    status = RunStatuses.Failure;
                }
                finally
                {
                    stopwatch.Stop();
                    algorithm.Dispose();
                }

                var request = ModelBuilder.CreateRunHistoryRequest()
                    .WithGraph(ActivatedGraph, range.Select(x => x.Position))
                    .WithRun(ActivatedGraphId, AlgorithmId)
                    .WithSubAlgorithms(subAlgorithms)
                    .WithStatistics(AlgorithmId, path,
                        visitedCount, status,
                        stopwatch.Elapsed);

                AppendStatistics(request.Statistics);

                await ExecuteSafe(async () =>
                {
                    var result = await service.CreateRunHistoryAsync(request)
                        .ConfigureAwait(false);
                    messenger.Send(new RunCreatedMessaged(result));
                }, logger.Error);
            }
        }
    }
}
