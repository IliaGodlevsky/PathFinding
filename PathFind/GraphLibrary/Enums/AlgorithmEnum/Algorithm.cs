using System.ComponentModel;

namespace GraphLibrary.Enums.AlgorithmEnum
{
    public enum Algorithms
    {
        [Description("Wide path find algorithm")]
        WidePathFind = 1,
        [Description("Deep path find algorithm")]
        DeepPathFind,
        [Description("Dijkstra algorithm")]
        DijkstraAlgorithm,
        [Description("A* algorithm")]
        AStarAlgorithm,
        [Description("Distance greedy algorithm")]
        DistanceGreedyAlgorithm,
        [Description("Value greedy algorithm")]
        ValueGreedyAlgorithm
    };
}
