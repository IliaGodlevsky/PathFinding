﻿using GraphLibrary.DistanceCalculator;
using GraphLibrary.Enums;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.PathFindingAlgorithm;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex.Interface;

namespace GraphLibrary.AlgoSelector
{
    public static class AlgorithmSelector
    {
        private static double AStartRelaxFunction(IVertex vertex, 
            IVertex neighbour, IVertex destination)
        {
            return neighbour.Cost + vertex.AccumulatedCost
                + Distance.GetChebyshevDistance(neighbour, destination);
        }

        private static double ValueDistanceGreedyFunction(IVertex vertex, IVertex destination)
        {
            return vertex.Cost + Distance.GetEuclideanDistance(vertex, destination);
        }

        public static IPathFindingAlgorithm GetPathFindAlgorithm(Algorithms algorithms, IGraph graph)
        {
            switch (algorithms)
            {
                case Algorithms.LiAlgorithm: return new LiAlgorithm() { Graph = graph };

                case Algorithms.DeepPathFind: return new DeepPathFindAlgorithm() { Graph = graph };

                case Algorithms.DijkstraAlgorithm: return new DijkstraAlgorithm() { Graph = graph };

                case Algorithms.AStarAlgorithm: return new DijkstraAlgorithm()
                {
                    Graph = graph,
                    RelaxFunction = (neighbour, vertex) => AStartRelaxFunction(vertex, neighbour, graph.End)
                };

                case Algorithms.DistanceGreedyAlgorithm: return new GreedyAlgorithm()
                {
                    Graph = graph,
                    GreedyFunction = vertex => Distance.GetEuclideanDistance(vertex, graph.End)
                };

                case Algorithms.ValueGreedyAlgorithm: return new GreedyAlgorithm()
                {
                    Graph = graph,
                    GreedyFunction = vertex => vertex.Cost
                };

                case Algorithms.ValueDistanceGreedyAlgorithm: return new GreedyAlgorithm()
                {
                    Graph = graph,
                    GreedyFunction = vertex => ValueDistanceGreedyFunction(vertex, graph.End)
                };

                default: return NullAlgorithm.GetInstance();
            }
        }
    }
}