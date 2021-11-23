using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Benchmark.Benchmarks
{
    public class DepthFirstAlgorithmBenchmark : AlgorithmBenchmarks
    {
        protected override IAlgorithm CreateAlgorithm(IEndPoints endPoints)
        {
            return new DepthFirstAlgorithm(endPoints);
        }
    }
}
