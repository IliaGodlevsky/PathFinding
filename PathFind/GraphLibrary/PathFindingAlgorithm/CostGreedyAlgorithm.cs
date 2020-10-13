using GraphLibrary.Graphs.Interface;
using System.ComponentModel;

namespace GraphLibrary.PathFindingAlgorithm
{
    [Description("Cost-greedy algorithm")]
    public class CostGreedyAlgorithm : DepthFirstAlgorithm
    {
        public CostGreedyAlgorithm(IGraph graph) : base(graph)
        {
            GreedyFunction = vertex => vertex.Cost;
        }
    }
}
