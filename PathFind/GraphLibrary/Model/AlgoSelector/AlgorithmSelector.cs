using GraphLibrary.Algorithm;
using GraphLibrary.DistanceCalculator;
using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Collection;
using GraphLibrary.PathFindAlgorithm;
using GraphLibrary.Vertex;

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

        public static IPathFindAlgorithm GetPathFindAlgorithm(Algorithms algorithms, Graph graph)
        {
            switch (algorithms)
            {
                //case Algorithms.WidePathFind: return new WidePathFindAlgorithm() { Graph = graph };
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
                    GreedyFunction = vertex => vertex.Cost + Distance.GetEuclideanDistance(vertex, graph.End)
                };
                default: return null;
            }
        }
    }
}
