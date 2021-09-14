using Algorithm.Algos.Algos;
using Algorithm.Algos.Enums;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Extensions
{
    public static class AlgorithmsEnumExtensions
    {
        public static IAlgorithm ToAlgorithm(this Algorithms self,
            IGraph graph, IIntermediateEndPoints endPoints)
        {
            switch (self)
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
    }
}
