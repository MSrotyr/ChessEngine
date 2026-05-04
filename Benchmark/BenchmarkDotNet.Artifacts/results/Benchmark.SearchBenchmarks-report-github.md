```

BenchmarkDotNet v0.15.8, Linux Fedora Linux 40 (Container Image)
AMD Ryzen 5 9600X 5.02GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.203
  [Host]     : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v4
  DefaultJob : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v4


```
| Method                        | Mean      | Error     | StdDev    | Median    |
|------------------------------ |----------:|----------:|----------:|----------:|
| GetRookMovesBenchmark         | 1.9731 ns | 0.0085 ns | 0.0075 ns | 1.9748 ns |
| GetRookMovesBenchmarkBaseline | 0.0426 ns | 0.0192 ns | 0.0401 ns | 0.0230 ns |
