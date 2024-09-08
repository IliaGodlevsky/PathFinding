using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.Exceptions;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models;
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
    internal sealed class CreateDijkstraRunViewModel : CreateRunViewModel, IRequireStepRuleViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<VertexModel> service;
        private readonly ILog logger;

        private (string Name, IStepRule Rule) stepRule;
        public (string Name, IStepRule Rule) StepRule
        {
            get => stepRule;
            set => this.RaiseAndSetIfChanged(ref stepRule, value);
        }

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

        public override ReactiveCommand<MouseEventArgs, Unit> CreateRunCommand { get; }

        public CreateDijkstraRunViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<VertexModel> service,
            ILog logger)
        {
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            CreateRunCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(CreateRun, CanCreate());
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
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

        private async Task CreateRun(MouseEventArgs e)
        {
            var range = await service.ReadRangeAsync(ActivatedGraphId);

            if (range.Count > 0)
            {
                var subAlgorithms = new List<CreateSubAlgorithmRequest>();
                int visitedCount = 0;
                var visitedVertices = new List<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)>();

                var pathfindingRange = range
                    .OrderBy(x => x.Order)
                    .Select(x => x.Position)
                    .Select(ActivatedGraph.Get)
                    .ToList();

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

                var path = NullGraphPath.Interface;
                string status = RunStatuses.Success;
                var stopwatch = new Stopwatch();
                var algorithm = new DijkstraAlgorithm(pathfindingRange, stepRule.Rule);

                algorithm.SubPathFound += OnSubPathFound;
                algorithm.VertexEnqueued += OnVertexEnqueued;

                stopwatch.Start();

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
                    .WithRun(ActivatedGraphId, AlgorithmNames.Dijkstra)
                    .WithSubAlgorithms(subAlgorithms)
                    .WithStatistics(AlgorithmNames.Dijkstra, path,
                        visitedCount, status, 
                        stopwatch.Elapsed, stepRule.Name);

                //await ExecuteSafe(async () =>
                //{
                //    var result = await Task.Run(() => service.CreateRunHistoryAsync(request))
                //        .ConfigureAwait(false);
                //    messenger.Send(new RunHistoryCreateMessage(result));
                //}, logger.Error);
            }
        }
    }
}
