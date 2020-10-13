using GraphLibrary.DistanceCalculating;
using GraphLibrary.Graphs.Interface;
using System.ComponentModel;

namespace GraphLibrary.PathFindingAlgorithm
{
    [Description("Distance greedy algorithm")]
    public class DistanceGreedyAlgoritm : DepthFirstAlgorithm
    {
        public DistanceGreedyAlgoritm(IGraph graph) : base(graph)
        {
            GreedyFunction = vertex => DistanceCalculator.GetChebyshevDistance(vertex, Graph.End);
        }
    }
}
