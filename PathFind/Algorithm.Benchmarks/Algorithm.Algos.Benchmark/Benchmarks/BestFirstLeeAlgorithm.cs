﻿using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using BenchmarkDotNet.Attributes;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Benchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class BestFirstLeeAlgorithmBenchmarks : AlgorithmBenchmarks
    {
        protected override IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new BestFirstLeeAlgorithm(graph, endPoints);
        }
    }
}
