using GraphLib.Graphs.Abstractions;
using System.ComponentModel;

namespace Algorithm.PathFindingAlgorithms
{
    [Description("Cost-greedy algorithm")]
    internal class CostGreedyAlgorithm : DepthFirstAlgorithm
    {
        public CostGreedyAlgorithm(IGraph graph) : base(graph)
        {
            GreedyFunction = vertex => (int)vertex.Cost;
        }
    }
}
