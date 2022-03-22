using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    public interface IAlgorithmFactory<out TAlgorithm> 
        where TAlgorithm : IAlgorithm
    {
        TAlgorithm Create(IEndPoints endPoints);
    }
}
