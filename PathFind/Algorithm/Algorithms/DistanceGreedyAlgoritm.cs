using Algorithm.Extensions;
using GraphLib.Interface;
using GraphLib.NullObjects;
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

        }

        protected override void TrySetDefaultGreedyFunction()
        {
            if (GreedyFunction == null)
            {
                GreedyFunction = vertex => vertex.CalculateChebyshevDistanceTo(End);
            }
        }
    }
}
