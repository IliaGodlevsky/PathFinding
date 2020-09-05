using System.ComponentModel;

namespace GraphLibrary.AlgorithmEnum
{
    public enum Algorithms : byte
    {
        [Description("Li algorithm")]
        LiAlgorithm = 1,
        [Description("Deep path find algorithm")]
        DeepPathFind,
        [Description("Dijkstra algorithm")]
        DijkstraAlgorithm,
        [Description("A* algorithm")]
        AStarAlgorithm,
        [Description("Distance greedy algorithm")]
        DistanceGreedyAlgorithm,
        [Description("Value greedy algorithm")]
        ValueGreedyAlgorithm,
        [Description("Value-distance greedy algorithm")]
        ValueDistanceGreedyAlgorithm
    }
}
