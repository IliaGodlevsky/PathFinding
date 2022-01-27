using BenchmarkDotNet.Running;
using System.Reflection;

namespace Algorithm.Algos.Benchmark
{
    class Program
    {
        private static Assembly CurrentAssembly => typeof(Program).Assembly;

        static void Main(string[] args) => BenchmarkRunner.Run(CurrentAssembly);
    }
}