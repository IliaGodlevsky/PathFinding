using GraphLibrary.Algorithm;
using GraphLibrary.Graph;

namespace GraphLibrary.Algorithms.AlgorithmFactory
{
    public class WidePathFindAlgorithmFactory : AbstractGraphFactory
    { 
        public WidePathFindAlgorithmFactory(AbstractGraph graph) : base(graph)
        {

        }

        public override IPathFindAlgorithm GetPathFindAlgorithm()
        {
            return new WidePathFindAlgorithm(graph);
        }
    }
}
