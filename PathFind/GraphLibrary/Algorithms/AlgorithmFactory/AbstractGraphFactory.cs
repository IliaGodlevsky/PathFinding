using GraphLibrary.Algorithm;
using GraphLibrary.Graph;

namespace GraphLibrary.Algorithms.AlgorithmFactory
{
    public abstract class AbstractGraphFactory : IAlgorithmFactory
    {
        protected AbstractGraph graph;
        public AbstractGraphFactory(AbstractGraph graph)
        {
            this.graph = graph;

        }
        public abstract IPathFindAlgorithm GetPathFindAlgorithm();
    }
}
