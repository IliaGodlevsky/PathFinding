using System;
using System.ComponentModel;

namespace Algorithm.Algos.Enums
{
    [Flags]
    public enum Algorithms : ulong
    {
        [Description("Lee algorithm")]
        LeeAlgorithm = 0,

        [Description("Lee algorithm (heuristic)")]
        BestFirstLeeAlgorithm = 2 << 0,

        [Description("Cost-first algorithm")]
        CostGreedyAlgorithm = 2 << 1,

        [Description("Depth-first algorithm")]
        DepthFirstAlgorithm = 2 << 2,

        [Description("Distance-first algorithm")]
        DistanceFirstAlgorithm = 2 << 3,

        [Description("Dijkstra's algorithm")]
        DijkstraAlgorithm = 2 << 4,

        [Description("A* algorithm")]
        AStarAlgorithm = 2 << 5,

        [Description("A* algorithm (modified)")]
        AStarModifiedAlgorithm = 2 << 6
    }
}