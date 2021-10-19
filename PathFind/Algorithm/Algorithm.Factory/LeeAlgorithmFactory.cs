using Algorithm.Algos.Algos;
using Algorithm.Base;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Description("Lee algorithm")]
    public sealed class LeeAlgorithmFactory : IAlgorithmFactory
    {
        public PathfindingAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new LeeAlgorithm(graph, endPoints);
        }
    }
}
