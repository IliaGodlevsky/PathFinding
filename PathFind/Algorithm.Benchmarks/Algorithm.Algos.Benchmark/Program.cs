using Algorithm.Algos.Benchmark.Heuristics;
using Algorithm.Algos.Benchmark.Pathfinding;
using Algorithm.Algos.Benchmark.Random;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<WaveAlgorithmBenchmarks>();
BenchmarkRunner.Run<HeuristicsBenchmarks>();
BenchmarkRunner.Run<RandomBenchmarks>();