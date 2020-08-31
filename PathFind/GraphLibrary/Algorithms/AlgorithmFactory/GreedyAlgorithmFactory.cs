using GraphLibrary.Algorithm;
using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.Algorithms.AlgorithmFactory
{
    public class GreedyAlgorithmFactory : AbstractGraphFactory
    {
        private readonly Func<IVertex, double> greedyFunction;
        public GreedyAlgorithmFactory(AbstractGraph graph,
            Func<IVertex,double> greedyFunction) : base(graph)
        {
            this.greedyFunction = greedyFunction;
        }

        public override IPathFindAlgorithm GetPathFindAlgorithm()
        {
            return new GreedyAlgorithm(graph)
            {
                GreedyFunction = greedyFunction
            };
        }
    }
}
