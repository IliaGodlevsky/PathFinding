using BenchmarkDotNet.Running;

namespace Pathfinding.Infrastructure.Business.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<WaveAlgorithmsBenchmarks>();
            //BenchmarkRunner.Run<HeuristicsBenchmarks>();
            //BenchmarkRunner.Run<StepRulesBenchmarks>();
            BenchmarkRunner.Run<SerializersBenchmarks>();
        }
    }
}
