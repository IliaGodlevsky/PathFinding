using Algorithm.Realizations.Heuristic.Angles;
using Algorithm.Realizations.Heuristic.Distances;
using BenchmarkDotNet.Attributes;
using GraphLib.Interfaces;
using GraphLib.Realizations.Neighbourhoods;
using GraphLib.TestRealizations.TestObjects;
using System.Collections.Generic;

namespace Algorithm.Algos.Benchmark.Heuristics
{
    [MemoryDiagnoser]
    public class HeuristicsBenchmarks
    {
        public IEnumerable<IVertex[]> Vertices()
        {
            yield return new IVertex[]
            {
                new TestVertex(new MooreNeighborhood(new TestCoordinate(0, 0)), new TestCoordinate(0, 0)),
                new TestVertex(new MooreNeighborhood(new TestCoordinate(29, 29)), new TestCoordinate(29, 29))
            };
            yield return new IVertex[]
            {
                new TestVertex(new MooreNeighborhood(new TestCoordinate(0, 1)), new TestCoordinate(0, 1)),
                new TestVertex(new MooreNeighborhood(new TestCoordinate(29, 15)), new TestCoordinate(29, 15))
            };
            yield return new IVertex[]
            {
                new TestVertex(new MooreNeighborhood(new TestCoordinate(1, 5)), new TestCoordinate(1, 5)),
                new TestVertex(new MooreNeighborhood(new TestCoordinate(15, 29)), new TestCoordinate(15, 29))
            };
            yield return new IVertex[]
            {
                new TestVertex(new MooreNeighborhood(new TestCoordinate(7, 6)), new TestCoordinate(7, 6)),
                new TestVertex(new MooreNeighborhood(new TestCoordinate(11, 17)), new TestCoordinate(11, 17))
            };
        }


        [Benchmark(Description = "Chebyshev distance")]
        [ArgumentsSource(nameof(Vertices))]
        public double ChebyshevDistanceHeuristicsBenchmark(IVertex from, IVertex to)
        {
            var algorithm = new ChebyshevDistance();
            return algorithm.Calculate(from, to);
        }

        [Benchmark(Baseline = true, Description = "Euclidian distance")]
        [ArgumentsSource(nameof(Vertices))]
        public double EuclidianDistanceHeuristicsBenchmark(IVertex from, IVertex to)
        {
            var algorithm = new EuclidianDistance();
            return algorithm.Calculate(from, to);
        }

        [Benchmark(Description = "Manhattan distance")]
        [ArgumentsSource(nameof(Vertices))]
        public double ManhattanDistanceHeuristicsBenchmark(IVertex from, IVertex to)
        {
            var algorithm = new ManhattanDistance();
            return algorithm.Calculate(from, to);
        }

        [Benchmark(Description = "Angle")]
        [ArgumentsSource(nameof(Vertices))]
        public double AngleHeuristicsBenchmark(IVertex from, IVertex to)
        {
            var algorithm = new AngleFunction(from);
            return algorithm.Calculate(from, to);
        }
    }
}
