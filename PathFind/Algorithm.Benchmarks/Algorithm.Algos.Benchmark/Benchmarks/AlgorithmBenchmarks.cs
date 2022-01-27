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

        private readonly IEndPoints endPoints10x10;
        private readonly IEndPoints endPoints20x20;
        private readonly IEndPoints endPoints30x30;
        private readonly IEndPoints endPoints40x40;
        private readonly IEndPoints endPoints50x50;

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

        [IterationCleanup]
        [Benchmark(Description ="Graph 10x10")]
        public void Graph10x10PathfindingTesting() => CreateAlgorithm(endPoints10x10).FindPath();

        [IterationCleanup]
        [Benchmark(Description = "Graph 20x20")]
        public void Graph20x20PathfindingTesting() => CreateAlgorithm(endPoints20x20).FindPath();

        [IterationCleanup]
        [Benchmark(Description = "Graph 30x30")]
        public void Graph30x30PathfindingTesting() => CreateAlgorithm(endPoints30x30).FindPath();

        [IterationCleanup]
        [Benchmark(Description = "Graph 40x40")]
        public void Graph40x40PathfindingTesting() => CreateAlgorithm(endPoints40x40).FindPath();

        [IterationCleanup]
        [Benchmark(Description = "Graph 50x50")]
        public void Graph50x50PathfindingTesting() => CreateAlgorithm(endPoints50x50).FindPath();

        private IEndPoints CreateEndPoints(IGraph graph) => new TestEndPoints(graph.Vertices.First(), graph.Vertices.Last());

        protected abstract IAlgorithm CreateAlgorithm(IEndPoints endPoints);
    }
}
