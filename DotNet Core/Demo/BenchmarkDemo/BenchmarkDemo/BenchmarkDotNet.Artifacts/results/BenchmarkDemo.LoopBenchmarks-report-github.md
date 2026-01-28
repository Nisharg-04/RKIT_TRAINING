```

BenchmarkDotNet v0.15.8, Windows 10 (10.0.17763.8146/1809/October2018Update/Redstone5)
Intel Xeon Gold 6138 CPU 2.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.417
  [Host]     : .NET 7.0.4 (7.0.4, 7.0.423.11508), X64 RyuJIT x86-64-v3
  Job-FQKHXO : .NET 7.0.4 (7.0.4, 7.0.423.11508), X64 RyuJIT x86-64-v3

LaunchCount=1  WarmupCount=3  

```
| Method      | Mean     | Error    | StdDev   | Allocated |
|------------ |---------:|---------:|---------:|----------:|
| ForeachLoop | 187.8 μs | 10.79 μs | 31.81 μs |         - |
| LinqSum     |       NA |       NA |       NA |        NA |

Benchmarks with issues:
  LoopBenchmarks.LinqSum: Job-FQKHXO(LaunchCount=1, WarmupCount=3)
