using Algorithm.Algos.Algos;
using Algorithm.Algos.Attributes;
using System.ComponentModel;

namespace Algorithm.Realizations.Enums
{
    public enum Algorithms
    {
        [AlgorithmType(typeof(LeeAlgorithm))]
        [Description("Lee algorithm")]
        LeeAlgorithm = 1,

        [AlgorithmType(typeof(BestFirstLeeAlgorithm))]
        [Description("Lee algorithm (heuristic")]
        BestFirstLeeAlgorithm = 2,

        [AlgorithmType(typeof(CostGreedyAlgorithm))]
        [Description("Cost-first algorithm")]
        CostGreedyAlgorithm = 3,

        [AlgorithmType(typeof(DepthFirstAlgorithm))]
        [Description("Depth-first algorithm")]
        DepthFirstAlgorithm = 4,

        [AlgorithmType(typeof(DistanceFirstAlgorithm))]
        [Description("Distance-first algorithm")]
        DistanceFirstAlgorithm = 5,

        [AlgorithmType(typeof(DijkstraAlgorithm))]
        [Description("Dijkstra's algorithm")]
        DijkstraAlgorithm = 6,

        [AlgorithmType(typeof(AStarAlgorithm))]
        [Description("A* algorithm")]
        AStarAlgorithm = 7,

        [AlgorithmType(typeof(AStarModified))]
        [Description("A* algorithm (modified)")]
        AStarModifiedAlgorithm = 8
    }
}