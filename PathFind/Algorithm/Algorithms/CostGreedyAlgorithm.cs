using GraphLib.Interface;
using GraphLib.NullObjects;
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
            set 
            { 
                graph = value; 
                GreedyFunction = vertex => vertex.Cost.CurrentCost; 
            }
        }

        public CostGreedyAlgorithm() : this(new NullGraph())
        {

        }

        public CostGreedyAlgorithm(IGraph graph) : base(graph)
        {

        }
    }
}
