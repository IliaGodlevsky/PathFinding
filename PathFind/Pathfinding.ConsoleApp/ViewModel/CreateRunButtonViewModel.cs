using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.Exceptions;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal abstract class CreateRunButtonViewModel : BaseViewModel
    {
        protected readonly IRequestService<VertexModel> service;
        protected readonly IMessenger messenger;
        protected readonly ILog logger;

        public abstract string AlgorithmId { get; }

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

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(ActivatedGraphId))
            {
                ActivatedGraph = Graph<VertexModel>.Empty;
                ActivatedGraphId = 0;
            }
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
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
        }

        protected abstract PathfindingProcess GetAlgorithm(IEnumerable<VertexModel> pathfindingRange);

        protected virtual void AppendStatistics(RunStatisticsModel model) { }

        private async Task CreateRun(MouseEventArgs e)
        {
            var range = await Task.Run(() => service.ReadRangeAsync(ActivatedGraphId))
                .ConfigureAwait(false);

            if (range.Count > 0)
            {
                var subAlgorithms = new List<CreateSubAlgorithmRequest>();
                int visitedCount = 0;
                var visitedVertices = new List<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)>();

                void AddSubAlgorithm(IReadOnlyCollection<Coordinate> path = null)
                {
                    ModelBuilder.CreateSubAlgorithmRequest()
                        .WithOrder(subAlgorithms.Count)
                        .WithPath(path ?? Array.Empty<Coordinate>())
                        .WithVisitedVertices(visitedVertices.ToArray())
                        .AddTo(subAlgorithms);
                    visitedVertices.Clear();
                }
                void OnVertexEnqueued(object sender, VerticesEnqueuedEventArgs e)
                {
                    visitedCount++;
                    visitedVertices.Add((e.Current, e.Enqueued));
                }
                void OnSubPathFound(object sender, SubPathFoundEventArgs args)
                {
                    AddSubAlgorithm(args.SubPath);
                }

                string status = RunStatuses.Success;
                var pathfindingRange = range
                    .OrderBy(x => x.Order)
                    .Select(x => ActivatedGraph.Get(x.Position))
                    .ToArray();

                using var algorithm = GetAlgorithm(pathfindingRange);

                algorithm.SubPathFound += OnSubPathFound;
                algorithm.VertexEnqueued += OnVertexEnqueued;

                var path = NullGraphPath.Interface;
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    path = await algorithm.FindPathAsync();
                }
                catch (PathfindingException ex)
                {
                    status = RunStatuses.Failure;
                    logger.Warn(ex);
                    AddSubAlgorithm();
                }
                catch (Exception ex)
                {
                    status = RunStatuses.Failure;
                    logger.Error(ex);
                    AddSubAlgorithm();
                }
                finally
                {
                    stopwatch.Stop();
                }

                var request = ModelBuilder.CreateRunHistoryRequest()
                    .WithGraph(ActivatedGraph, range.Select(x => x.Position))
                    .WithRun(ActivatedGraphId, AlgorithmId)
                    .WithSubAlgorithms(subAlgorithms)
                    .WithStatistics(AlgorithmId, path,
                        visitedCount, status, stopwatch.Elapsed);

                AppendStatistics(request.Statistics);

                await ExecuteSafe(async () =>
                {
                    var result = await Task.Run(() => service.CreateRunHistoryAsync(request))
                        .ConfigureAwait(false);
                    messenger.Send(new RunCreatedMessaged(result));
                }, logger.Error).ConfigureAwait(false);
            }
            else
            {
                logger.Info("Pathfinding range is not set");
            }
        }
    }
}
