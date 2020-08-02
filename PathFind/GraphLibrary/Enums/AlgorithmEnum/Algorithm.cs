using System.ComponentModel;

namespace GraphLibrary.Enums.AlgorithmEnum
{
    public enum Algorithms
    {
        [Description("Wide path find algorithm")]
        WidePathFind,
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
    };
}
