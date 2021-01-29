using Algorithm.Extensions;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using System.ComponentModel;

namespace Algorithm.Algorithms
{
    [Description("Distance greedy algorithm")]
    public class DistanceGreedyAlgoritm : DepthFirstAlgorithm
    {
        public DistanceGreedyAlgoritm() : this(new NullGraph())
        {

        }

        public DistanceGreedyAlgoritm(IGraph graph) : base(graph)
        {
            GreedyFunction = vertex => vertex.CalculateChebyshevDistanceTo(Graph.End);
        }
    }
}
