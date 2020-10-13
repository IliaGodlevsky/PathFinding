using GraphLibrary.DistanceCalculating;
using GraphLibrary.Graphs.Interface;
using System.ComponentModel;

namespace GraphLibrary.PathFindingAlgorithm
{
    [Description("Cost-distance greedy algorithm")]
    public class CostDistanceGreedyAlgorithm : DepthFirstAlgorithm
    {
        public CostDistanceGreedyAlgorithm(IGraph graph) : base(graph)
        {
            GreedyFunction = vertex => vertex.Cost + DistanceCalculator.GetChebyshevDistance(vertex, graph.End);
        }
    }
}
