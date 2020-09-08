using System.ComponentModel;

namespace GraphLibrary.Enums
{
    public enum Algorithms : byte
    {
        [Description("Li algorithm")]
        LiAlgorithm = 1,
        [Description("Dijkstra algorithm")]
        DijkstraAlgorithm,
        [Description("A* algorithm")]
        AStarAlgorithm,
        [Description("Greedy algorithm (distance)")]
        DistanceGreedyAlgorithm,
        [Description("Greedy algorithm (cost)")]
        ValueGreedyAlgorithm,
        [Description("Greedy algorithm (distance & cost)")]
        ValueDistanceGreedyAlgorithm,
        [Description("Greedy algorithm (deep)")]
        DeepPathFind
    }
}
