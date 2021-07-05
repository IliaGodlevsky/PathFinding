using Algorithm.Interfaces;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;
using Plugins.DijkstraALgorithm;

namespace Plugins.ImprovedDijkstraAlgorithm
{
    [ClassName("Dijkstra algorithm (improved)")]
    public class ImprovedDijkstraAlgorithm : DijkstraAlgorithm
    {
        public ImprovedDijkstraAlgorithm(IGraph graph, IEndPoints endPoints) 
            : base(graph, endPoints)
        {
        }

        public ImprovedDijkstraAlgorithm(IGraph graph, IEndPoints endPoints, IStepRule stepRule) 
            : base(graph, endPoints, stepRule)
        {

        }
    }
}
