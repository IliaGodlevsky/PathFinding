using GraphLibrary.Enums.AlgorithmEnum;
using SearchAlgorythms.Algorithm;
using SearchAlgorythms.DistanceCalculator;
using SearchAlgorythms.Graph;

namespace GraphLibrary.PathFindAlgorithmSelector
{
    public static class AlgorithmSelector
    {
        public static IPathFindAlgorithm GetPathFindAlgorithm(Algorithms algorithms, AbstractGraph graph)
        {
            switch (algorithms)
            {
                case Algorithms.WidePathFind: return new WidePathFindAlgorithm(graph);
                case Algorithms.BestFirstPathFind: return new BestFirstAlgorithm(graph);
                case Algorithms.DijkstraAlgorithm: return new DijkstraAlgorithm(graph);
                case Algorithms.AStarAlgorithm: return new AStarAlgorithm(graph)
                {
                    HeuristicFunction = DistanceCalculator.GetChebyshevDistance
                };
                case Algorithms.GreedyAlgorithm: return new GreedyAlgorithm(graph);
                default: return null;
            }
        }
    }
}
