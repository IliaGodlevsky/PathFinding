﻿using GraphLibrary.Algorithm;
using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.Graph;
using GraphLibrary.PathFindAlgorithm;
namespace GraphLibrary.PathFindAlgorithmSelector
{
    public static class AlgorithmSelector
    {
        public static IPathFindAlgorithm GetPathFindAlgorithm(Algorithms algorithms, AbstractGraph graph)
        {
            switch (algorithms)
            {
                case Algorithms.WidePathFind: return new WidePathFindAlgorithm(graph);
                case Algorithms.DeepPathFind: return new DeepPathFindAlgorithm(graph);
                case Algorithms.DijkstraAlgorithm: return new DijkstraAlgorithm(graph);
                case Algorithms.AStarAlgorithm: return new AStarAlgorithm(graph)
                {
                    HeuristicFunction = (neighbour, vertex) =>
                    int.Parse(neighbour.Text) + vertex.Value + DistanceCalculator.DistanceCalculator.GetChebyshevDistance(neighbour, graph.End)
                };
                case Algorithms.DistanceGreedyAlgorithm: return new GreedyAlgorithm(graph)
                {
                    GreedyFunction = vertex => DistanceCalculator.DistanceCalculator.GetEuclideanDistance(vertex, graph.End)
                };
                case Algorithms.ValueGreedyAlgorithm: return new GreedyAlgorithm(graph)
                {
                    GreedyFunction = vertex => int.Parse(vertex.Text)
                };
                default: return null;
            }
        }
    }
}