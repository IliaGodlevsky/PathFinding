using Algorithm.Interfaces;
using BenchmarkDotNet.Attributes;
using GraphLib.Interfaces;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using System.Linq;

namespace Algorithm.Algos.Benchmark.Benchmarks
{
    public abstract class AlgorithmBenchmarks
    {
        private readonly IGraph graph10x10;
        private readonly IGraph graph20x20;
        private readonly IGraph graph30x30;
        private readonly IGraph graph40x40;
        private readonly IGraph graph50x50;

        private readonly IIntermediateEndPoints endPoints10x10;
        private readonly IIntermediateEndPoints endPoints20x20;
        private readonly IIntermediateEndPoints endPoints30x30;
        private readonly IIntermediateEndPoints endPoints40x40;
        private readonly IIntermediateEndPoints endPoints50x50;

        protected AlgorithmBenchmarks()
        {
            var testGraphAssemble = new TestGraphAssemble();
            graph10x10 = testGraphAssemble.AssembleGraph(0, 10, 10);
            graph20x20 = testGraphAssemble.AssembleGraph(0, 20, 20);
            graph30x30 = testGraphAssemble.AssembleGraph(0, 30, 30);
            graph40x40 = testGraphAssemble.AssembleGraph(0, 40, 40);
            graph50x50 = testGraphAssemble.AssembleGraph(0, 50, 50);
            endPoints10x10 = CreateEndPoints(graph10x10);
            endPoints20x20 = CreateEndPoints(graph20x20);
            endPoints30x30 = CreateEndPoints(graph30x30);
            endPoints40x40 = CreateEndPoints(graph40x40);
            endPoints50x50 = CreateEndPoints(graph50x50);
        }

        [IterationCleanup][Benchmark]
        public void Graph10x10PathfindingTesting() => CreateAlgorithm(graph10x10, endPoints10x10).FindPath();

        [IterationCleanup][Benchmark]
        public void Graph20x20PathfindingTesting() => CreateAlgorithm(graph20x20, endPoints20x20).FindPath();

        [IterationCleanup][Benchmark]
        public void Graph30x30PathfindingTesting() => CreateAlgorithm(graph30x30, endPoints30x30).FindPath();

        [IterationCleanup][Benchmark]
        public void Graph40x40PathfindingTesting() => CreateAlgorithm(graph40x40, endPoints40x40).FindPath();

        [IterationCleanup][Benchmark]
        public void Graph50x50PathfindingTesting() => CreateAlgorithm(graph50x50, endPoints50x50).FindPath();

        private IIntermediateEndPoints CreateEndPoints(IGraph graph) => new TestEndPoints(graph.Vertices.First(), graph.Vertices.Last());

        protected abstract IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints);
    }
}
