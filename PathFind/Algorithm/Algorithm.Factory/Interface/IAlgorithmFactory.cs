using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace Algorithm.Factory.Interface
{
    public interface IAlgorithmFactory<out TAlgorithm> where TAlgorithm : IAlgorithm
    {
        TAlgorithm Create(IEndPoints endPoints);
    }
}
