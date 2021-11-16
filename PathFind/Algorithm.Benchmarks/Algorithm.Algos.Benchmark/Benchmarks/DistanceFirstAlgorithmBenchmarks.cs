using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Benchmark.Benchmarks
{
    public class DistanceFirstAlgorithmBenchmarks : AlgorithmBenchmarks
    {
        protected override IAlgorithm CreateAlgorithm(IGraph graph, IEndPoints endPoints)
        {
            return new DistanceFirstAlgorithm(graph, endPoints);
        }
    }
}
