using GraphLibrary.DistanceCalculator;
using GraphLibrary.Enums;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.PathFindingAlgorithm;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex.Interface;

namespace GraphLibrary.AlgoSelector
{
    public static class AlgorithmSelector
    {
        private static double CastAndDistanceGreedyFunction(IVertex vertex, IVertex destination)
        {
            return vertex.Cost + Distance.GetEuclideanDistance(vertex, destination);
        }
        
        public static IPathFindingAlgorithm GetPathFindAlgorithm(Algorithms algorithms, IGraph graph)
        {
            switch (algorithms)
            {
                case Algorithms.LiAlgorithm: return new LeeAlgorithm() { Graph = graph };
                case Algorithms.DeepPathFind: return new GreedyAlgorithm() 
                { 
                    Graph = graph,
                    GreedyFunction = vertex => Distance.GetChebyshevDistance(vertex, graph.Start)
                };
                case Algorithms.DijkstraAlgorithm: return new DijkstraAlgorithm() { Graph = graph };
                case Algorithms.AStarAlgorithm: return new AStarAlgorithm()
                {
                    Graph = graph,
                    HeuristicFunction = vertex => Distance.GetChebyshevDistance(vertex, graph.End)
                };
                case Algorithms.AStarModified: return new AStarModified()
                {
                    Graph = graph,
                    HeuristicFunction = vertex => Distance.GetChebyshevDistance(vertex, graph.End)
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
                    GreedyFunction = vertex => CastAndDistanceGreedyFunction(vertex, graph.End)
                };
                default: return NullAlgorithm.Instance;
            }
        }
    }
}
