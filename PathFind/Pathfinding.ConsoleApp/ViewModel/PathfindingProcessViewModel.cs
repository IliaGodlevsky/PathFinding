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
    internal abstract class PathfindingProcessViewModel : BaseViewModel
    {
        protected readonly IRequestService<GraphVertexModel> service;
        protected readonly IMessenger messenger;
        protected readonly ILog logger;

        public abstract string AlgorithmId { get; }

        public ReactiveCommand<MouseEventArgs, Unit> StartAlgorithmCommand { get; }

        private int graphId;
        private int GraphId
        {
            get => graphId;
            set => this.RaiseAndSetIfChanged(ref graphId, value);
        }

        private IGraph<GraphVertexModel> graph;
        private IGraph<GraphVertexModel> Graph
        {
            get => graph;
            set => this.RaiseAndSetIfChanged(ref graph, value);
        }

        private IObservable<bool> CanStartAlgorithm()
        {
            return this.WhenAnyValue(
                x => x.GraphId,
                x => x.Graph,
                (id, graph) => id > 0 && graph != null);
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            Graph = msg.Graph;
            GraphId = msg.GraphId;
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(GraphId))
            {
                Graph = Graph<GraphVertexModel>.Empty;
                GraphId = 0;
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

        protected virtual void AppendStatistics(RunStatisticsModel model) { }

        private async Task StartAlgorithm(MouseEventArgs e)
        {
            var msg = new QueryPathfindingRangeMessage();
            messenger.Send(msg);
            var pathfindingRange = msg.PathfindingRange;

            if (pathfindingRange.Count > 0)
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
                void OnVertexProcessed(object sender, VerticesProcessedEventArgs e)
                {
                    visitedCount++;
                    visitedVertices.Add((e.Current, e.Enqueued));
                }
                void OnSubPathFound(object sender, SubPathFoundEventArgs args)
                {
                    AddSubAlgorithm(args.SubPath);
                }

                string status = RunStatuses.Success;

                using var algorithm = GetAlgorithm(pathfindingRange);

                algorithm.SubPathFound += OnSubPathFound;
                algorithm.VertexProcessed += OnVertexProcessed;

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
                    .WithGraph(Graph, pathfindingRange.Select(x => x.Position))
                    .WithRun(GraphId, AlgorithmId)
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
