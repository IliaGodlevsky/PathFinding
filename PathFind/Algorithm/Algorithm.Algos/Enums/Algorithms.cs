using System.ComponentModel;

namespace Algorithm.Realizations.Enums
{
    public enum Algorithms
    {
        [Description("Lee algorithm")]
        LeeAlgorithm = 1,

        [Description("Lee algorithm (heuristic")]
        BestFirstLeeAlgorithm = 2,

        [Description("Cost-first algorithm")]
        CostGreedyAlgorithm = 3,

        [Description("Depth-first algorithm")]
        DepthFirstAlgorithm = 4,

        [Description("Distance-first algorithm")]
        DistanceFirstAlgorithm = 5,

        [Description("Dijkstra's algorithm")]
        DijkstraAlgorithm = 6,

        [Description("A* algorithm")]
        AStarAlgorithm = 7,

        [Description("A* algorithm (modified)")]
        AStarModifiedAlgorithm = 8
    }
}