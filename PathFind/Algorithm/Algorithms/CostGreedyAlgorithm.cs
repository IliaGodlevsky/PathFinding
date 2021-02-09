using GraphLib.Interface;
using GraphLib.NullObjects;
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

        }

        protected override void TrySetDefaultGreedyFunction()
        {
            if (GreedyFunction == null)
            {
                GreedyFunction = vertex => vertex.Cost.CurrentCost;
            }
        }
    }
}
