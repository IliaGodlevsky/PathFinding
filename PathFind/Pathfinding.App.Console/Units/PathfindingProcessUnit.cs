using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.Exceptions;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Requests.Create;
using Shared.Extensions;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingProcessUnit : Unit, ICanReceiveMessage
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<Vertex> requestService;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private readonly IInput<ConsoleKey> input;
        private readonly ILog log;

        private readonly List<(ICoordinate, IReadOnlyList<ICoordinate>)> visitedVertices = new();
        private readonly List<CreateSubAlgorithmRequest> subAlgorithms = new();

        private GraphModel<Vertex> Graph = null;
        private int visited = 0;
        private string algorithmStatus = string.Empty;

        public PathfindingProcessUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IInput<ConsoleKey> input,
            IMessenger messenger,
            ILog log,
            IRequestService<Vertex> requestService) : base(menuItems)
        {
            this.messenger = messenger;
            this.log = log;
            this.rangeBuilder = rangeBuilder;
            this.input = input;
            this.requestService = requestService;
        }

        private async void FindPath(AlgorithmStartInfoMessage msg)
        {
            using (Cursor.HideCursor())
            {
                var range = rangeBuilder.Range.ToReadOnly();
                var algorithm = msg.Factory.Create(range);
                var path = NullGraphPath.Interface;
                var stopwatch = new Stopwatch();
                try
                {
                    using (Disposable.Use(stopwatch.Stop))
                    {
                        PrepareForPathfinding(algorithm);
                        algorithmStatus = AlgorithmStatuses.Started;
                        stopwatch.Restart();
                        path = await algorithm.FindPathAsync();
                        algorithmStatus = AlgorithmStatuses.Succeeded;
                    }
                }
                catch (AlgorithmInterruptedException ex)
                {
                    log.Warn(ex.Message);
                    algorithmStatus = AlgorithmStatuses.Interrupted;
                }
                catch (PathfindingException ex)
                {
                    log.Warn(ex.Message);
                    algorithmStatus = AlgorithmStatuses.Failed;
                }
                finally
                {
                    await OnFinished(msg, path, stopwatch.Elapsed);
                    algorithm.Dispose();
                }
            }
        }

        private void ClearColors(PathfindingProcessUnit unit, ClearColorsMessage m)
        {
            Graph.Graph.RestoreVerticesVisualState();
            rangeBuilder.Range.RestoreVerticesVisualState();
            var msg = new StatisticsLineMessage(string.Empty);
            messenger.Send(msg, Tokens.AppLayout);
        }

        private void SetGraph(GraphMessage msg)
        {
            Graph = msg.Graph;
        }

        private void OnSubPathFound(object sender, SubPathFoundEventArgs args)
        {
            var subAlgorithm = new CreateSubAlgorithmRequest()
            {
                Path = args.SubPath,
                Visited = visitedVertices.ToReadOnly(),
                Order = subAlgorithms.Count
            };
            subAlgorithms.Add(subAlgorithm);
            visitedVertices.Clear();
        }

        private async Task OnFinished(AlgorithmStartInfoMessage msg,
            IGraphPath path,
            TimeSpan elapsed)
        {
            CreateAlgorithmRunHistoryRequest runRequest = new()
            {
                Run = new()
                {
                    GraphId = Graph.Id,
                    AlgorithmId = msg.AlgorithmId
                },
                GraphState = new()
                {
                    Obstacles = Graph.Graph.GetObstaclesCoordinates().ToReadOnly(),
                    Costs = Graph.Graph.GetCosts(),
                    Range = rangeBuilder.Range.GetCoordinates().ToReadOnly()
                },
                SubAlgorithms = subAlgorithms.ToReadOnly(),
                Statistics = new()
                {
                    ResultStatus = algorithmStatus,
                    AlgorithmId = msg.AlgorithmId,
                    Elapsed = elapsed,
                    StepRule = msg.StepRule,
                    Heuristics = msg.Heuristics,
                    Spread = msg.Spread,
                    Cost = path.Cost,
                    Steps = path.Count,
                    Visited = visited
                }
            };
            subAlgorithms.Clear();
            visitedVertices.Clear();
            visited = 0;
            algorithmStatus = string.Empty;
            await requestService.CreateRunHistoryAsync(runRequest.Enumerate());
            var model = await requestService.ReadRunInfoAsync(runRequest.Run.Id);
        }

        private void OnVerticesEnqueued(object sender, VerticesEnqueuedEventArgs args)
        {
            visitedVertices.Add((args.Current, args.Enqueued));
            visited++;
        }

        private void PrepareForPathfinding(PathfindingProcess algorithm)
        {
            algorithm.SubPathFound += OnSubPathFound;
            algorithm.VertexEnqueued += OnVerticesEnqueued;
            //var lineMsg = new StatisticsLineMessage(statistics.Name);
            //messenger.Send(lineMsg, Tokens.AppLayout);
            //var algorithmMsg = new AlgorithmMessage(algorithm);
            //messenger.SendMany(algorithmMsg, Tokens.Visualization,
            //    Tokens.Statistics, Tokens.History);
            //var algoStatMsg = new StatisticsMessage(statistics);
            //messenger.Send(algoStatMsg, Tokens.History);
            //var statMsg = new StatisticsMessage(statistics);
            //messenger.Send(statMsg, Tokens.Statistics);
        }

        public void RegisterHandlers(IMessenger messenger)
        {
            messenger.Register<PathfindingProcessUnit, AlgorithmStartInfoMessage>(this, Tokens.Pathfinding, FindPath);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.Register<PathfindingProcessUnit, ClearColorsMessage>(this, ClearColors);
        }
    }
}