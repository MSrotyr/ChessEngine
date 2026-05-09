using Benchmark;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<SearchBenchmarks>();
BenchmarkRunner.Run<RookSearchBenchmarks>();
BenchmarkRunner.Run<BishopSearchBenchmarks>();
BenchmarkRunner.Run<KnightSearchBenchmarks>();