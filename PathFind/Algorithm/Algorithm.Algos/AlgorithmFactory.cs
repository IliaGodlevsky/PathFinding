using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using Algorithm.Realizations.Enums;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Realizations
{
    public static class AlgorithmFactory
    {
        public static IAlgorithm CreateAlgorithm(Algorithms algorithm, IGraph graph, IEndPoints endPoints)
        {
            switch (algorithm)
            {
                case Algorithms.LeeAlgorithm: return new LeeAlgorithm(graph, endPoints);
                case Algorithms.BestFirstLeeAlgorithm: return new BestFirstLeeAlgorithm(graph, endPoints);
                case Algorithms.CostGreedyAlgorithm: return new CostGreedyAlgorithm(graph, endPoints);
                case Algorithms.DepthFirstAlgorithm: return new DepthFirstAlgorithm(graph, endPoints);
                case Algorithms.DistanceFirstAlgorithm: return new DistanceFirstAlgorithm(graph, endPoints);
                case Algorithms.DijkstraAlgorithm: return new DijkstraAlgorithm(graph, endPoints);
                case Algorithms.AStarAlgorithm: return new AStarAlgorithm(graph, endPoints);
                case Algorithms.AStarModifiedAlgorithm: return new AStarModified(graph, endPoints);
                default: throw new ArgumentException($"Couldn't create algorithm from {algorithm}");
            }
        }
    }
}