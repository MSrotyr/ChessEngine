```

BenchmarkDotNet v0.15.8, Linux Fedora Linux 40 (Container Image)
AMD Ryzen 5 9600X 5.02GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.203
  [Host]     : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v4
  DefaultJob : .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v4


```
| Method                     | Mean          | Error      | StdDev     |
|--------------------------- |--------------:|-----------:|-----------:|
| GetPossibleMovesBenchmark  | 1,097.0111 ns | 16.5304 ns | 14.6538 ns |
| GetWhitePawnMovesBenchmark |    15.0396 ns |  0.0691 ns |  0.0577 ns |
| GetKnightMovesBenchmark    |     2.9135 ns |  0.0226 ns |  0.0189 ns |
| GetBishopMovesBenchmark    |    15.1973 ns |  0.0503 ns |  0.0446 ns |
| GetRookMovesBenchmark      |    15.6575 ns |  0.0289 ns |  0.0256 ns |
| GetQueenMovesBenchmark     |    13.7176 ns |  0.0153 ns |  0.0144 ns |
| GetKingMovesBenchmark      |     0.2131 ns |  0.0025 ns |  0.0021 ns |
