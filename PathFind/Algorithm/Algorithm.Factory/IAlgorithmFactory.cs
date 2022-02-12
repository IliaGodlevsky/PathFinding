using Algorithm.Base;
using GraphLib.Interfaces;

namespace Algorithm.Factory
{
    public interface IAlgorithmFactory
    {
        PathfindingAlgorithm Create(IEndPoints endPoints);
    }
}
