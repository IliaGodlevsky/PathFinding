using GraphLibrary.Algorithm;
using GraphLibrary.DistanceCalculator;
using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Graph;
using GraphLibrary.PathFindAlgorithm;

namespace GraphLibrary.AlgoSelector
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
                    neighbour.Cost + vertex.AccumulatedCost +
                    Distance.GetChebyshevDistance(neighbour, graph.End)
                };
                case Algorithms.DistanceGreedyAlgorithm: return new GreedyAlgorithm(graph)
                {
                    GreedyFunction = vertex => Distance.GetEuclideanDistance(vertex, graph.End)
                };
                case Algorithms.ValueGreedyAlgorithm: return new GreedyAlgorithm(graph)
                {
                    GreedyFunction = vertex => vertex.Cost
                };
                case Algorithms.ValueDistanceGreedyAlgorithm: return new GreedyAlgorithm(graph)
                {
                    GreedyFunction = vertex => vertex.Cost + 
                    Distance.GetEuclideanDistance(vertex, graph.End)
                };
                default: return null;
            }
        }
    }
}
