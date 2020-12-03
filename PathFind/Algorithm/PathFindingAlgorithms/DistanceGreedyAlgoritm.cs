using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using System.ComponentModel;

namespace Algorithm.PathFindingAlgorithms
{
    [Description("Distance greedy algorithm")]
    internal class DistanceGreedyAlgoritm : DepthFirstAlgorithm
    {
        public DistanceGreedyAlgoritm(IGraph graph) : base(graph)
        {
            GreedyFunction = vertex => vertex.GetChebyshevDistanceTo(Graph.End);
        }
    }
}
