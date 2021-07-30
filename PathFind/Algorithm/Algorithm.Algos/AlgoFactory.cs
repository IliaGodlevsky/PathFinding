using Algorithm.Algos.Algos;
using Algorithm.Algos.Enums;
using Algorithm.Common;
using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Algos
{
    public static class AlgoFactory
    {
        public static IAlgorithm CreateAlgorithm(Algorithms algorithm,
            IGraph graph, IEndPoints endPoints)
        {
            switch (algorithm)
            {
                case Algorithms.LeeAlgorithm:
                    return new LeeAlgorithm(graph, endPoints);

                case Algorithms.BestFirstLeeAlgorithm:
                    return new BestFirstLeeAlgorithm(graph, endPoints);

                case Algorithms.CostGreedyAlgorithm:
                    return new CostGreedyAlgorithm(graph, endPoints);

                case Algorithms.DepthFirstAlgorithm:
                    return new DepthFirstAlgorithm(graph, endPoints);

                case Algorithms.DistanceFirstAlgorithm:
                    return new DistanceFirstAlgorithm(graph, endPoints);

                case Algorithms.DijkstraAlgorithm:
                    return new DijkstraAlgorithm(graph, endPoints);

                case Algorithms.AStarAlgorithm:
                    return new AStarAlgorithm(graph, endPoints);

                case Algorithms.AStarModifiedAlgorithm:
                    return new AStarModified(graph, endPoints);

                default:
                    return new NullAlgorithm();
            }
        }

        public static IEnumerable<IAlgorithm> CreateAlgorithms(Algorithms algorithm,
            IGraph graph, IEndPoints endPoints)
        {
            var algorithms = Enum.GetValues(typeof(Algorithms)).Cast<Algorithms>();
            foreach(var algo in algorithms)
            {
                if((algo & algorithm) == algo)
                {
                    yield return CreateAlgorithm(algo, graph, endPoints);
                }
            }
        }
    }
}
