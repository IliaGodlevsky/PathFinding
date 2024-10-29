using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AlgorithmRunReviewFieldViewModel : AlgorithmRunBaseViewModel
    {
        private readonly ILog log;

        public AlgorithmRunReviewFieldViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog log)
            : base(messenger)
        {
            messenger.Register<RunActivatedMessage>(this, OnRunActivated);
            this.log = log;
        }

        private void OnRunActivated(object recipient, RunActivatedMessage msg)
        {
            var subAlgorithms = new List<SubAlgorithmModel>();
            var visitedVertices = new List<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)>();
            var rangeMsg = new QueryPathfindingRangeMessage();
            messenger.Send(rangeMsg);
            var rangeCoordinates = rangeMsg.PathfindingRange;

            void AddSubAlgorithm(IReadOnlyCollection<Coordinate> path = null)
            {
                subAlgorithms.Add(new()
                {
                    Order = subAlgorithms.Count,
                    Visited = visitedVertices,
                    Path = path ?? Array.Empty<Coordinate>()
                });
                visitedVertices = new();
            }
            void OnVertexProcessed(object sender, VerticesProcessedEventArgs e)
            {
                visitedVertices.Add((e.Current, e.Enqueued));
            }
            void OnSubPathFound(object sender, SubPathFoundEventArgs args)
            {
                AddSubAlgorithm(args.SubPath);
            }

            var graph = CreateGraph();
            var range = rangeCoordinates.Select(x=> graph[x]).ToArray();

            try
            {
                var algorithm = GetAlgorithm(msg.Run, range);
                algorithm.SubPathFound += OnSubPathFound;
                algorithm.VertexProcessed += OnVertexProcessed;
                try
                {
                    algorithm.FindPath();
                }
                finally
                {
                    algorithm.SubPathFound -= OnSubPathFound;
                    algorithm.VertexProcessed -= OnVertexProcessed;
                }
            }
            catch (NotImplementedException ex)
            {
                log.Warn(ex, ex.Message);
                AddSubAlgorithm();
            }
            catch (Exception)
            {
                AddSubAlgorithm();
            }
            Vertices = GetVerticesStates(subAlgorithms, rangeCoordinates, graph);
            GraphState = graph.Values;
        }

        private IHeuristic GetHeuristic(RunStatisticsModel statistics)
        {
            switch (statistics.Heuristics)
            {
                case HeuristicNames.Euclidian: return new EuclidianDistance().WithWeight(statistics.Weight);
                case HeuristicNames.Chebyshev: return new ChebyshevDistance().WithWeight(statistics.Weight);
                case HeuristicNames.Diagonal: return new DiagonalDistance().WithWeight(statistics.Weight);
                case HeuristicNames.Manhattan: return new ManhattanDistance().WithWeight(statistics.Weight);
                case HeuristicNames.Cosine: return new CosineDistance().WithWeight(statistics.Weight);
                default: throw new NotImplementedException($"Unknown heuristic: {statistics.Heuristics}");
            }
        }

        private IStepRule GetStepRule(RunStatisticsModel statistics)
        {
            switch (statistics.StepRule)
            {
                case StepRuleNames.Default: return new DefaultStepRule();
                case StepRuleNames.Landscape: return new LandscapeStepRule();
                default: throw new NotImplementedException($"Unknown step rule: {statistics.StepRule}");
            }
        }

        private PathfindingProcess GetAlgorithm(RunStatisticsModel statistics, IReadOnlyCollection<RunVertexModel> range)
        {
            switch (statistics.AlgorithmName)
            {
                case AlgorithmNames.Dijkstra: return new DijkstraAlgorithm(range, GetStepRule(statistics));
                case AlgorithmNames.BidirectDijkstra: return new BidirectDijkstraAlgorithm(range, GetStepRule(statistics));
                case AlgorithmNames.DepthFirst: return new DepthFirstAlgorithm(range);
                case AlgorithmNames.AStar: return new AStarAlgorithm(range, GetStepRule(statistics), GetHeuristic(statistics));
                case AlgorithmNames.BidirectAStar: return new BidirectAStarAlgorithm(range, GetStepRule(statistics), GetHeuristic(statistics));
                case AlgorithmNames.CostGreedy: return new CostGreedyAlgorithm(range, GetStepRule(statistics));
                case AlgorithmNames.DistanceFirst: return new DistanceFirstAlgorithm(range, GetHeuristic(statistics));
                case AlgorithmNames.Snake: return new SnakeAlgorithm(range);
                case AlgorithmNames.AStarGreedy: return new AStarGreedyAlgorithm(range, GetHeuristic(statistics), GetStepRule(statistics));
                case AlgorithmNames.Lee: return new LeeAlgorithm(range);
                case AlgorithmNames.BidirectLee: return new BidirectLeeAlgorithm(range);
                case AlgorithmNames.AStarLee: return new AStarLeeAlgorithm(range, GetHeuristic(statistics));
                default: throw new NotImplementedException($"Unknown algorithm name: {statistics.AlgorithmName}");
            }
        }
    }
}
