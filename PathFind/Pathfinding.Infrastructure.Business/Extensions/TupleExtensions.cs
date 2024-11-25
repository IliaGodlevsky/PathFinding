using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class TupleExtensions
    {
        public static PathfindingProcess Assemble(this (
            IEnumerable<IPathfindingVertex> Range,
            Domain.Core.Algorithms Algorithm,
            StepRules? StepRule, 
            HeuristicFunctions? Heuristics, 
            double? Weight) values)
        {
            IStepRule GetStepRule()
            {
                return values.StepRule switch
                {
                    StepRules.Default => new DefaultStepRule(),
                    StepRules.Landscape => new LandscapeStepRule(),
                    null => throw new ArgumentNullException(nameof(values.StepRule)),
                    _ => throw new NotImplementedException($"Unknown step rule: {values.StepRule}")
                };
            }
            IHeuristic GetHeuristic()
            {
                return values.Heuristics switch
                {
                    HeuristicFunctions.Euclidian => new EuclidianDistance().WithWeight(values.Weight),
                    HeuristicFunctions.Chebyshev => new ChebyshevDistance().WithWeight(values.Weight),
                    HeuristicFunctions.Diagonal => new DiagonalDistance().WithWeight(values.Weight),
                    HeuristicFunctions.Manhattan => new ManhattanDistance().WithWeight(values.Weight),
                    HeuristicFunctions.Cosine => new CosineDistance().WithWeight(values.Weight),
                    null => throw new ArgumentNullException(nameof(values.Heuristics)),
                    _ => throw new NotImplementedException($"Unknown heuristic: {values.Heuristics}")
                };
            }

            return values.Algorithm switch
            {
                Domain.Core.Algorithms.AStar => new AStarAlgorithm(values.Range, GetStepRule(), GetHeuristic()),
                Domain.Core.Algorithms.AStarGreedy => new AStarGreedyAlgorithm(values.Range, GetHeuristic(), GetStepRule()),
                Domain.Core.Algorithms.AStarLee => new AStarLeeAlgorithm(values.Range, GetHeuristic()),
                Domain.Core.Algorithms.BidirectAStar => new BidirectAStarAlgorithm(values.Range, GetStepRule(), GetHeuristic()),
                Domain.Core.Algorithms.BidirectDijkstra => new BidirectDijkstraAlgorithm(values.Range, GetStepRule()),
                Domain.Core.Algorithms.BidirectLee => new BidirectLeeAlgorithm(values.Range),
                Domain.Core.Algorithms.CostGreedy => new CostGreedyAlgorithm(values.Range, GetStepRule()),
                Domain.Core.Algorithms.DepthFirst => new DepthFirstAlgorithm(values.Range),
                Domain.Core.Algorithms.Dijkstra => new DijkstraAlgorithm(values.Range, GetStepRule()),
                Domain.Core.Algorithms.DistanceFirst => new DistanceFirstAlgorithm(values.Range, GetHeuristic()),
                Domain.Core.Algorithms.Lee => new LeeAlgorithm(values.Range),
                Domain.Core.Algorithms.Snake => new SnakeAlgorithm(values.Range, GetHeuristic()),
                _ => throw new NotImplementedException($"Uknown algorithm: {values.Algorithm}")
            };
        }
    }
}
