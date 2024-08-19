using BenchmarkDotNet.Attributes;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Benchmarks.Data;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Benchmarks
{
    [MemoryDiagnoser]
    public class WaveAlgorithmsBenchmarks
    {
        private static IEnumerable<BenchmarkVertex> range;

        [GlobalSetup]
        public void Setup()
        {
            range = BenchmarkRange.Interface;
        }

        [Benchmark(Baseline = true)]
        public void DijkstraAlgorithmBenchmark()
        {
            var algorithm = new DijkstraAlgorithm(range);

            algorithm.FindPath();
        }

        [Benchmark]
        public void AStarAlgorithmBenchmark()
        {
            var algorithm = new AStarAlgorithm(range);

            algorithm.FindPath();
        }

        [Benchmark]
        public void IDAStarAlgorithmBenchmark()
        {
            var algorithm = new IDAStarAlgorithm(range);

            algorithm.FindPath();
        }
    }
}
