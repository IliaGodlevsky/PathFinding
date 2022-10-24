using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using BenchmarkDotNet.Attributes;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Benchmark.Pathfinding
{
    [MemoryDiagnoser]
    public class WaveAlgorithmBenchmarks : AlgorithmsBenchmarks
    {
        [ArgumentsSource(nameof(Arguments))]
        [Benchmark(Baseline = true, Description = "Dijkstra's algorithm")]
        public IGraphPath DijkstraAlgorithmBenchmark(IPathfindingRange endPoints)
        {
            var algorithm = new DijkstraAlgorithm(endPoints);
            return algorithm.FindPath();
        }

        [ArgumentsSource(nameof(Arguments))]
        [Benchmark(Description = "A* algorithm")]
        public IGraphPath AStarAlgorithmBenchmark(IPathfindingRange endPoints)
        {
            var algorithm = new AStarAlgorithm(endPoints);
            return algorithm.FindPath();
        }

        [ArgumentsSource(nameof(Arguments))]
        [Benchmark(Description = "IDA* algorithm")]
        public IGraphPath IDAStarAlgorithmBenchmark(IPathfindingRange endPoints)
        {
            var algorithm = new IDAStarAlgorithm(endPoints);
            return algorithm.FindPath();
        }
    }
}
