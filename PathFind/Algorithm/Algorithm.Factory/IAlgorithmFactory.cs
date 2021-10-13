using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace Algorithm.Factory
{
    public interface IAlgorithmFactory
    {
        IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints);
    }
}
