using System.ComponentModel;

namespace Algorithm.Algos.Enums
{
    public enum Algorithms
    {
        [Description("Lee algorithm            ")]
        LeeAlgorithm = 0,

        [Description("Lee algorithm (heuristic)")]
        BestFirstLeeAlgorithm = 1,

        [Description("Cost-first algorithm     ")]
        CostGreedyAlgorithm = 2,

        [Description("Depth-first algorithm    ")]
        DepthFirstAlgorithm = 3,

        [Description("Distance-first algorithm ")]
        DistanceFirstAlgorithm = 4,

        [Description("Dijkstra's algorithm     ")]
        DijkstraAlgorithm = 5,

        [Description("A* algorithm             ")]
        AStarAlgorithm = 6,

        [Description("A* algorithm (modified)  ")]
        AStarModifiedAlgorithm = 7
    }
}