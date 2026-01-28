using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Linq;
using System.Collections.Generic;

[MemoryDiagnoser]
[SimpleJob(launchCount: 1, warmupCount: 1)]
public class TestBench
{
    private List<int> numbers;

    [GlobalSetup]
    public void Setup() => numbers = Enumerable.Range(1, 10_000).ToList();

    [Benchmark]
    public int ForeachLoop()
    {
        int sum = 0;
        foreach (var n in numbers) sum += n;
        return sum;
    }

    private long result; // store benchmark result

    [Benchmark]
    public void LinqSum()
    {
        result = numbers.Sum(n => (long)n);
    }

}

class Program
{
    static void Main() => BenchmarkRunner.Run<TestBench>();
}
