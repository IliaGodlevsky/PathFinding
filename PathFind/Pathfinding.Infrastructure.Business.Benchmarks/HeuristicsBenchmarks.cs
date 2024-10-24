using BenchmarkDotNet.Attributes;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Benchmarks.Data;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Benchmarks
{
    public class HeuristicsBenchmarks
    {
        private static BenchmarkVertex first;
        private static BenchmarkVertex second;

        [GlobalSetup]
        public void Setup()
        {
            first = new BenchmarkVertex(new Coordinate(2, 4));
            second = new BenchmarkVertex(new Coordinate(7, 11));
        }

        [Benchmark]
        public void ChebyshevDistanceBenchmark()
        {
            var chebyshev = new ChebyshevDistance();

            chebyshev.Calculate(first, second);
        }

        [Benchmark(Baseline = true)]
        public void ManhattanDistanceBenchmark()
        {
            var chebyshev = new ManhattanDistance();

            chebyshev.Calculate(first, second);
        }

        [Benchmark]
        public void EuclidianDistanceBenchmark()
        {
            var chebyshev = new EuclidianDistance();

            chebyshev.Calculate(first, second);
        }

        [Benchmark]
        public void CosineDistanceBenchmark()
        {
            var chebyshev = new CosineDistance();

            chebyshev.Calculate(first, second);
        }
    }
}
