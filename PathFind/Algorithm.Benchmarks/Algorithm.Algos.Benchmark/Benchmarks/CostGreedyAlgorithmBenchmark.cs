using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Benchmark.Benchmarks
{
    public class CostGreedyAlgorithmBenchmark : AlgorithmBenchmarks
    {
        protected override IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new CostGreedyAlgorithm(graph, endPoints);
        }
    }
}
