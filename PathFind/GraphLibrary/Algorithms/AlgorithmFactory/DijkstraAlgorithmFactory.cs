using GraphLibrary.Algorithm;
using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.Algorithms.AlgorithmFactory
{
    public class DijkstraAlgorithmFactory : AbstractGraphFactory
    {
        private readonly Func<IVertex, IVertex, double> relaxFunction;

        public DijkstraAlgorithmFactory(AbstractGraph graph,
            Func<IVertex, IVertex, double> relaxFunction) : base(graph)
        {
            this.relaxFunction = relaxFunction;
        }

        public override IPathFindAlgorithm GetPathFindAlgorithm()
        {
            return new DijkstraAlgorithm(graph)
            {
                RelaxFunction = relaxFunction
            };
        }
    }
}
