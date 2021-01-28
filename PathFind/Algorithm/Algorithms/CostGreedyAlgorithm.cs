using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using System.ComponentModel;

namespace Algorithm.Algorithms
{
    [Description("Cost-greedy algorithm")]
    public class CostGreedyAlgorithm : DepthFirstAlgorithm
    {
        private IGraph graph;
        public override IGraph Graph
        {
            get => graph;
            set { graph = value; GreedyFunction = vertex => (int)vertex.Cost; }
        }

        public CostGreedyAlgorithm() : this(new NullGraph())
        {

        }

        public CostGreedyAlgorithm(IGraph graph) : base(graph)
        {
            
        }
    }
}
