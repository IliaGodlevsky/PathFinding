using BenchmarkDotNet.Attributes;
using Random.Extensions;
using Random.Interface;
using Random.Realizations.Generators;

namespace Algorithm.Algos.Benchmark.Random
{
    public class RandomBenchmarks
    {
        [Params(1000,2000,4000,8000,16000,32000,64000)]
        public int NumberOfIterations { get; set; }

        [Benchmark(Baseline = true, Description ="Pseudo random algorithm")]
        public void PseudoRandomBenchmark() => Benchmark(NumberOfIterations, new PseudoRandom());

        [Benchmark(Description = "Knuth random algorithm")]
        public void KnuthRandomBenchmark() => Benchmark(NumberOfIterations, new KnuthRandom());

        [Benchmark(Description = "Crypto random algorithm")]
        public void CryptoRandomBenchmark() => Benchmark(NumberOfIterations, new CryptoRandom());

        private void Benchmark(int iterations, IRandom random)
        {
            while (iterations-- > 0) random.NextUint();
        }

    }
}
