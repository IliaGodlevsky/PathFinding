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
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AlgorithmRunReviewFieldViewModel : AlgorithmRunBaseViewModel
    {
        private readonly ILog log;

        public AlgorithmRunReviewFieldViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog log)
            : base(messenger)
        {
            messenger.Register<RunActivatedMessage>(this, async (r, msg) => await OnRunActivated(r, msg));
            this.log = log;
        }

        private async Task OnRunActivated(object recipient, RunActivatedMessage msg)
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
            var range = rangeCoordinates.Select(x => graph[x]).ToArray();

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
            Vertices = await Task.Run(() => GetVerticesStates(subAlgorithms, rangeCoordinates, graph));
            GraphState = graph.Values;
        }

        private IHeuristic GetHeuristic(RunStatisticsModel statistics)
        {
            return statistics.Heuristics switch
            {
                HeuristicNames.Euclidian => new EuclidianDistance().WithWeight(statistics.Weight),
                HeuristicNames.Chebyshev => new ChebyshevDistance().WithWeight(statistics.Weight),
                HeuristicNames.Diagonal => new DiagonalDistance().WithWeight(statistics.Weight),
                HeuristicNames.Manhattan => new ManhattanDistance().WithWeight(statistics.Weight),
                HeuristicNames.Cosine => new CosineDistance().WithWeight(statistics.Weight),
                _ => throw new NotImplementedException($"Unknown heuristic: {statistics.Heuristics}"),
            };
        }

        private IStepRule GetStepRule(RunStatisticsModel statistics)
        {
            return statistics.StepRule switch
            {
                StepRuleNames.Default => new DefaultStepRule(),
                StepRuleNames.Landscape => new LandscapeStepRule(),
                _ => throw new NotImplementedException($"Unknown step rule: {statistics.StepRule}"),
            };
        }

        private PathfindingProcess GetAlgorithm(RunStatisticsModel statistics, IReadOnlyCollection<RunVertexModel> range)
        {
            return statistics.AlgorithmName switch
            {
                AlgorithmNames.Dijkstra => new DijkstraAlgorithm(range, GetStepRule(statistics)),
                AlgorithmNames.BidirectDijkstra => new BidirectDijkstraAlgorithm(range, GetStepRule(statistics)),
                AlgorithmNames.DepthFirst => new DepthFirstAlgorithm(range),
                AlgorithmNames.AStar => new AStarAlgorithm(range, GetStepRule(statistics), GetHeuristic(statistics)),
                AlgorithmNames.BidirectAStar => new BidirectAStarAlgorithm(range, GetStepRule(statistics), GetHeuristic(statistics)),
                AlgorithmNames.CostGreedy => new CostGreedyAlgorithm(range, GetStepRule(statistics)),
                AlgorithmNames.DistanceFirst => new DistanceFirstAlgorithm(range, GetHeuristic(statistics)),
                AlgorithmNames.Snake => new SnakeAlgorithm(range),
                AlgorithmNames.AStarGreedy => new AStarGreedyAlgorithm(range, GetHeuristic(statistics), GetStepRule(statistics)),
                AlgorithmNames.Lee => new LeeAlgorithm(range),
                AlgorithmNames.BidirectLee => new BidirectLeeAlgorithm(range),
                AlgorithmNames.AStarLee => new AStarLeeAlgorithm(range, GetHeuristic(statistics)),
                _ => throw new NotImplementedException($"Unknown algorithm name: {statistics.AlgorithmName}"),
            };
        }
    }
}
