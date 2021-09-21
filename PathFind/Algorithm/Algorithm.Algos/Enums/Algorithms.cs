using EnumerationValues.Attributes;
using System;
using System.ComponentModel;

namespace Algorithm.Algos.Enums
{
    [Flags]
    public enum Algorithms : ulong // up to 64 values
    {
        [EnumFetchIgnore]
        None = 0,

        [Description("Lee algorithm")]
        LeeAlgorithm = 2 << 0,

        [Description("Lee algorithm (heuristic)")]
        BestFirstLeeAlgorithm = 2 << 1,

        [Description("Cost-first algorithm")]
        CostGreedyAlgorithm = 2 << 2,

        [Description("Depth-first algorithm")]
        DepthFirstAlgorithm = 2 << 3,

        [Description("Distance-first algorithm")]
        DistanceFirstAlgorithm = 2 << 4,

        [Description("Dijkstra's algorithm")]
        DijkstraAlgorithm = 2 << 5,

        [Description("A* algorithm")]
        AStarAlgorithm = 2 << 6,

        [Description("A* algorithm (modified)")]
        AStarModifiedAlgorithm = 2 << 7
    }
}