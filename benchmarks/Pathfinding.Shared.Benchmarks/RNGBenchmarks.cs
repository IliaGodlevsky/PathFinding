using BenchmarkDotNet.Attributes;
using Pathfinding.Shared.Random;

namespace Pathfinding.Shared.Benchmarks
{
    [MemoryDiagnoser]
    public class RNGBenchmarks
    {
        [Benchmark]
        public static void CryptoRandomBenchmark()
        {
            var random = new CryptoRandom();

            random.NextUInt();
        }

        [Benchmark]
        public static void XorshiftRandomBenchmarks()
        {
            var random = new XorshiftRandom();

            random.NextUInt();
        }

        [Benchmark(Baseline = true)]
        public static void CongruentialRandomBenchmark()
        {
            var random = new CongruentialRandom();

            random.NextUInt();
        }

        [Benchmark]
        public static void KnuthRandomBenchmark()
        {
            var random = new KnuthRandom();

            random.NextUInt();
        }
    }
}
