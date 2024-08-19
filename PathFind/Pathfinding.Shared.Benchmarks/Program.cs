using BenchmarkDotNet.Running;

namespace Pathfinding.Shared.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<RNGBenchmarks>();
        }
    }
}
