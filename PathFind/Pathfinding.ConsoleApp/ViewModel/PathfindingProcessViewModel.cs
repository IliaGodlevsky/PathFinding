using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.Exceptions;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models;
using Pathfinding.Service.Interface.Models.Read;
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
    internal abstract class PathfindingProcessViewModel : BaseViewModel
    {
        protected readonly IRequestService<GraphVertexModel> service;
        protected readonly IMessenger messenger;
        protected readonly ILog logger;

        public abstract string AlgorithmName { get; }

        public ReactiveCommand<MouseEventArgs, Unit> StartAlgorithmCommand { get; }

        private GraphModel<GraphVertexModel> graph = GraphModel<GraphVertexModel>.Empty;
        public GraphModel<GraphVertexModel> Graph
        {
            get => graph;
            set => this.RaiseAndSetIfChanged(ref graph, value);
        }

        protected virtual IObservable<bool> CanStartAlgorithm()
        {
            return this.WhenAnyValue(
                x => x.Graph.Graph,
                x => x.Graph.Id,
                (graph, id) => id > 0 && graph != null);
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            Graph = msg.Graph;
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(Graph.Id))
            {
                Graph = new();
            }
        }

        protected PathfindingProcessViewModel(IRequestService<GraphVertexModel> service,
            IMessenger messenger,
            ILog logger)
        {
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            StartAlgorithmCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(StartAlgorithm, CanStartAlgorithm());
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
        }

        protected abstract PathfindingProcess GetAlgorithm(IEnumerable<GraphVertexModel> pathfindingRange);

        protected virtual void AppendStatistics(CreateStatisticsRequest request) { }

        private async Task StartAlgorithm(MouseEventArgs e)
        {
            var msg = new QueryPathfindingRangeMessage();
            messenger.Send(msg);
            var pathfindingRange = msg.PathfindingRange;

            if (pathfindingRange.Count > 1)
            {
                var subAlgorithms = new List<SubAlgorithmModel>();
                int visitedCount = 0;
                var visitedVertices = new List<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)>();

                void AddSubAlgorithm(IReadOnlyCollection<Coordinate> path = null)
                {
                    subAlgorithms.Add(new()
                    {
                        Order = subAlgorithms.Count,
                        Visited = visitedVertices.ToArray(),
                        Path = path ?? Array.Empty<Coordinate>()
                    });
                    visitedCount += visitedVertices.Count;
                    visitedVertices.Clear();
                }
                void OnVertexProcessed(object sender, VerticesProcessedEventArgs e)
                {
                    visitedVertices.Add((e.Current, e.Enqueued));
                }
                void OnSubPathFound(object sender, SubPathFoundEventArgs args)
                {
                    AddSubAlgorithm(args.SubPath);
                }

                string status = RunStatuses.Success;

                var vertices = pathfindingRange.Select(x => Graph.Graph.Get(x)).ToList();
                var algorithm = GetAlgorithm(vertices);

                algorithm.SubPathFound += OnSubPathFound;
                algorithm.VertexProcessed += OnVertexProcessed;

                var path = NullGraphPath.Interface;
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    path = algorithm.FindPath();
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
                    algorithm.SubPathFound -= OnSubPathFound;
                    algorithm.VertexProcessed -= OnVertexProcessed;
                }

                var request = ModelBuilder.CreateStatisticsRequest()
                    .WithStatistics(Graph.Id, AlgorithmName, path,
                        visitedCount, status, stopwatch.Elapsed);

                AppendStatistics(request);

                await ExecuteSafe(async () =>
                {
                    var result = await service.CreateStatisticsAsync(request)
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
