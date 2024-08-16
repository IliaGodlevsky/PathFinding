using BenchmarkDotNet.Attributes;
using Pathfinding.Shared.Random;

namespace Pathfinding.Shared.Benchmarks
{
    [MemoryDiagnoser]
    public class RNGBenchmarks
    {
        [Benchmark]
        public void CryptoRandomBenchmark()
        {
            var random = new CryptoRandom();

            random.NextUInt();
        }

        [Benchmark]
        public void XorshiftRandomBenchmarks()
        {
            var random = new XorshiftRandom();

            random.NextUInt();
        }

        [Benchmark(Baseline = true)]
        public void CongruentialRandomBenchmark()
        {
            var random = new CongruentialRandom();

            random.NextUInt();
        }

        [Benchmark]
        public void KnuthRandomBenchmark()
        {
            var random = new KnuthRandom();

            random.NextUInt();
        }

        [Benchmark]
        public void ThreadSafeCryptoRandomBenchmark()
        {
            var random = new ThreadSafeRandom(new CryptoRandom());

            random.NextUInt();
        }

        [Benchmark]
        public void ThreadSafeXorshiftRandomBenchmarks()
        {
            var random = new ThreadSafeRandom(new XorshiftRandom());

            random.NextUInt();
        }

        [Benchmark]
        public void ThreadSafeCongruentialRandomBenchmark()
        {
            var random = new ThreadSafeRandom(new CongruentialRandom());

            random.NextUInt();
        }

        [Benchmark]
        public void ThreadSafeKnuthRandomBenchmark()
        {
            var random = new ThreadSafeRandom(new KnuthRandom());

            random.NextUInt();
        }
    }
}
