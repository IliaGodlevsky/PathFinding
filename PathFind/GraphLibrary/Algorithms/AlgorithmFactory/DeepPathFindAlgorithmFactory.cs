using GraphLibrary.Algorithm;
using GraphLibrary.Graph;
using GraphLibrary.PathFindAlgorithm;

namespace GraphLibrary.Algorithms.AlgorithmFactory
{
    public class DeepPathFindAlgorithmFactory : AbstractGraphFactory
    {
        public DeepPathFindAlgorithmFactory(AbstractGraph graph) : base(graph)
        {

        }

        public override IPathFindAlgorithm GetPathFindAlgorithm()
        {
            return new DeepPathFindAlgorithm(graph);
        }
    }
}
