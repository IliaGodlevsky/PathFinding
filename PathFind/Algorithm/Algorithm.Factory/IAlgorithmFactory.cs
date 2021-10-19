using Algorithm.Base;
using GraphLib.Interfaces;

namespace Algorithm.Factory
{
    public interface IAlgorithmFactory
    {
        PathfindingAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints);
    }
}
