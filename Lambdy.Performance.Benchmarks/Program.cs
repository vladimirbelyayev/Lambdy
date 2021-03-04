using BenchmarkDotNet.Running;
using Lambdy.Performance.Benchmarks.SampleBenchmarks;

namespace Lambdy.Performance.Benchmarks
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Md5VsSha256>();
        }
    }
}