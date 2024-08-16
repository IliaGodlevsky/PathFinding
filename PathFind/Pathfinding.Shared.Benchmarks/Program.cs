using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Pathfinding.Shared.Benchmarks
{
    [MemoryDiagnoser]
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<RNGBenchmarks>();
        }
    }
}
