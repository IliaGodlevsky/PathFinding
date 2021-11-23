using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using BenchmarkDotNet.Attributes;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Benchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class DijkstraAlgorithmBenchmarks : AlgorithmBenchmarks
    {
        protected override IAlgorithm CreateAlgorithm(IEndPoints endPoints)
        {
            return new DijkstraAlgorithm(endPoints);
        }
    }
}
