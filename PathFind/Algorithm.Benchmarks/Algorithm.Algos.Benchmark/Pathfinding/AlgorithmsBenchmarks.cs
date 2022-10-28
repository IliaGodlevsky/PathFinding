using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using System.Collections.Generic;

namespace Algorithm.Algos.Benchmark.Pathfinding
{
    public abstract class AlgorithmsBenchmarks
    {
        private const int Limit = 10;

        public IEnumerable<IEndPoints> Arguments()
        {
            var graphAssemble = new TestGraphAssemble();
            for (int i = 1; i <= Limit; i += 2)
            {
                int dimension = i * 10;
                var graph = graphAssemble.AssembleGraph(0, dimension, dimension);
                InitializeNeighbours(graph);
                yield return new TestEndPoints(graph);
            }
        }

        private void InitializeNeighbours(IGraph graph)
        {
            foreach (var vertex in graph)
            {
                var neighbours = vertex.Neighbours;
            }
        }
    }
}
