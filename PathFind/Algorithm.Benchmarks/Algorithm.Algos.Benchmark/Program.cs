using Algorithm.Algos.Benchmark.Benchmarks;
using BenchmarkDotNet.Running;

namespace Algorithm.Algos.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<DijkstraAlgorithmBenchmarks>();
            BenchmarkRunner.Run<AStarAlgorithmBenchmarks>();
            BenchmarkRunner.Run<AStarModifiedBenchmarks>();
            BenchmarkRunner.Run<LeeAlgorithmBenchmarks>();
            BenchmarkRunner.Run<BestFirstLeeAlgorithmBenchmarks>();
            BenchmarkRunner.Run<CostGreedyAlgorithmBenchmark>();
            BenchmarkRunner.Run<DepthFirstAlgorithmBenchmark>();
            BenchmarkRunner.Run<DistanceFirstAlgorithmBenchmarks>();
        }
    }
}