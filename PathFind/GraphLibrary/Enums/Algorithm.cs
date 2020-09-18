using System.ComponentModel;

namespace GraphLibrary.Enums
{
    public enum Algorithms : byte
    {
        [Description("Lee algorithm")]
        LeeAlgorithm = 1,
        [Description("Dijkstra algorithm")]
        DijkstraAlgorithm,
        [Description("A* algorithm")]
        AStarAlgorithm,
        [Description("A* algorithm (modified)")]
        AStarModified,
        [Description("Greedy algorithm (distance)")]
        DistanceGreedyAlgorithm,
        [Description("Greedy algorithm (cost)")]
        ValueGreedyAlgorithm,
        [Description("Greedy algorithm (distance & cost)")]
        ValueDistanceGreedyAlgorithm,
        [Description("Greedy algorithm (depth-first)")]
        DeepPathFind
    }
}
