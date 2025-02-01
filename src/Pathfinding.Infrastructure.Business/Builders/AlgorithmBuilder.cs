﻿using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models;
using Algorithm = Pathfinding.Domain.Core.Algorithms;

namespace Pathfinding.Infrastructure.Business.Builders
{
    public sealed class AlgorithmBuilder
    {
        public static PathfindingProcess CreateAlgorithm(
            IEnumerable<IPathfindingVertex> range, 
            IAlgorithmBuildInfo info)
        {
            return info.Algorithm switch
            {
                Algorithm.AStar => new AStarAlgorithm(range, GetStepRule(info.StepRule),
                    GetHeuristic(info.Heuristics, info.Weight)),
                Algorithm.AStarGreedy => new AStarGreedyAlgorithm(range, 
                    GetHeuristic(info.Heuristics, info.Weight), GetStepRule(info.StepRule)),
                Algorithm.AStarLee => new AStarLeeAlgorithm(range, 
                    GetHeuristic(info.Heuristics, info.Weight)),
                Algorithm.BidirectAStar => new BidirectAStarAlgorithm(range,
                    GetStepRule(info.StepRule), GetHeuristic(info.Heuristics, info.Weight)),
                Algorithm.BidirectDijkstra => new BidirectDijkstraAlgorithm(range, 
                    GetStepRule(info.StepRule)),
                Algorithm.BidirectLee => new BidirectLeeAlgorithm(range),
                Algorithm.CostGreedy => new CostGreedyAlgorithm(range, 
                    GetStepRule(info.StepRule)),
                Algorithm.DepthFirst => new DepthFirstAlgorithm(range),
                Algorithm.Dijkstra => new DijkstraAlgorithm(range, 
                    GetStepRule(info.StepRule)),
                Algorithm.DistanceFirst => new DistanceFirstAlgorithm(range, 
                    GetHeuristic(info.Heuristics, info.Weight)),
                Algorithm.Lee => new LeeAlgorithm(range),
                Algorithm.Snake => new SnakeAlgorithm(range, new ManhattanDistance()),
                _ => throw new NotImplementedException($"Uknown algorithm: {info.Algorithm}")
            };
        }

        private static IStepRule GetStepRule(StepRules? stepRules)
        {
            return stepRules switch
            {
                StepRules.Default => new DefaultStepRule(),
                StepRules.Landscape => new LandscapeStepRule(),
                _ => throw new NotImplementedException($"Unknown step rule: {stepRules}")
            };
        }

        private static IHeuristic GetHeuristic(HeuristicFunctions? heuristic, double? weight)
        {
            return heuristic switch
            {
                HeuristicFunctions.Euclidian => new EuclidianDistance().WithWeight(weight),
                HeuristicFunctions.Chebyshev => new ChebyshevDistance().WithWeight(weight),
                HeuristicFunctions.Diagonal => new DiagonalDistance().WithWeight(weight),
                HeuristicFunctions.Manhattan => new ManhattanDistance().WithWeight(weight),
                HeuristicFunctions.Cosine => new CosineDistance().WithWeight(weight),
                _ => throw new NotImplementedException($"Unknown heuristic: {heuristic}")
            };
        }
    }
}
