using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using System.ComponentModel;

namespace Algorithm.Algorithms
{
    [Description("Cost-greedy algorithm")]
    public class CostGreedyAlgorithm : DepthFirstAlgorithm
    {
        public CostGreedyAlgorithm() : this(new NullGraph())
        {

        }

        public CostGreedyAlgorithm(IGraph graph) : base(graph)
        {
            GreedyFunction = vertex => (int)vertex.Cost;
        }
    }
}
