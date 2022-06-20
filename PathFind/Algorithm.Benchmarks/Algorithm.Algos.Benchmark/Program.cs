using Algorithm.Algos.Benchmark.Heuristics;
using Algorithm.Algos.Benchmark.Pathfinding;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<WaveAlgorithmBenchmarks>();
BenchmarkRunner.Run<HeuristicsBenchmarks>();