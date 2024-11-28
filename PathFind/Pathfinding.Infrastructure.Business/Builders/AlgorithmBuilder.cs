using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models;
using System;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Builders
{
    public sealed class AlgorithmBuilder
    {
        private readonly Domain.Core.Algorithms algorithm;
        private StepRules? stepRules;
        private HeuristicFunctions? heuristic;
        private double? weight;

        public static AlgorithmBuilder TakeAlgorithm(Domain.Core.Algorithms algorithm) 
            => new(algorithm);

        public AlgorithmBuilder WithAlgorithmInfo(IAlgorithmBuildInfo info)
        {
            weight = info.Weight;
            heuristic = info.Heuristics;
            stepRules = info.StepRule;
            return this;
        }

        public PathfindingProcess Build(IEnumerable<IPathfindingVertex> range)
        {
            return algorithm switch
            {
                Domain.Core.Algorithms.AStar => new AStarAlgorithm(range, GetStepRule(), GetHeuristic()),
                Domain.Core.Algorithms.AStarGreedy => new AStarGreedyAlgorithm(range, GetHeuristic(), GetStepRule()),
                Domain.Core.Algorithms.AStarLee => new AStarLeeAlgorithm(range, GetHeuristic()),
                Domain.Core.Algorithms.BidirectAStar => new BidirectAStarAlgorithm(range, GetStepRule(), GetHeuristic()),
                Domain.Core.Algorithms.BidirectDijkstra => new BidirectDijkstraAlgorithm(range, GetStepRule()),
                Domain.Core.Algorithms.BidirectLee => new BidirectLeeAlgorithm(range),
                Domain.Core.Algorithms.CostGreedy => new CostGreedyAlgorithm(range, GetStepRule()),
                Domain.Core.Algorithms.DepthFirst => new DepthFirstAlgorithm(range),
                Domain.Core.Algorithms.Dijkstra => new DijkstraAlgorithm(range, GetStepRule()),
                Domain.Core.Algorithms.DistanceFirst => new DistanceFirstAlgorithm(range, GetHeuristic()),
                Domain.Core.Algorithms.Lee => new LeeAlgorithm(range),
                Domain.Core.Algorithms.Snake => new SnakeAlgorithm(range, GetHeuristic()),
                _ => throw new NotImplementedException($"Uknown algorithm: {algorithm}")
            };
        }

        private IStepRule GetStepRule()
        {
            return stepRules switch
            {
                StepRules.Default => new DefaultStepRule(),
                StepRules.Landscape => new LandscapeStepRule(),
                _ => throw new NotImplementedException($"Unknown step rule: {stepRules}")
            };
        }


        private IHeuristic GetHeuristic()
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

        private AlgorithmBuilder(Domain.Core.Algorithms algorithm) => this.algorithm = algorithm;
    }
}
