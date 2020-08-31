using GraphLibrary.Algorithm;

namespace GraphLibrary.Algorithms.AlgorithmFactory
{
    public interface IAlgorithmFactory
    {
        IPathFindAlgorithm GetPathFindAlgorithm();
    }
}
