namespace Pathfinding.AlgorithmLib.Benchmarks.Pathfinding
{
    [MemoryDiagnoser]
    public class WaveAlgorithmBenchmarks : AlgorithmsBenchmarks
    {
        [ArgumentsSource(nameof(Ranges))]
        [Benchmark(Baseline = true, Description = "Dijkstra's algorithm")]
        public IGraphPath DijkstraAlgorithmBenchmark(IEnumerable<IVertex> range)
        {
            var algorithm = new DijkstraAlgorithm(range);
            return algorithm.FindPath();
        }

        [ArgumentsSource(nameof(Ranges))]
        [Benchmark(Description = "A* algorithm")]
        public IGraphPath AStarAlgorithmBenchmark(IEnumerable<IVertex> range)
        {
            var algorithm = new AStarAlgorithm(range);
            return algorithm.FindPath();
        }

        [ArgumentsSource(nameof(Ranges))]
        [Benchmark(Description = "IDA* algorithm")]
        public IGraphPath IDAStarAlgorithmBenchmark(IEnumerable<IVertex> range)
        {
            var algorithm = new IDAStarAlgorithm(range);
            return algorithm.FindPath();
        }

        [ArgumentsSource(nameof(Ranges))]
        [Benchmark(Description = "Random algorithm")]
        public IGraphPath RandomAlgorithmBenchmark(IEnumerable<IVertex> range)
        {
            var algorithm = new RandomAlgorithm(range, random);
            return algorithm.FindPath();
        }
    }
}
