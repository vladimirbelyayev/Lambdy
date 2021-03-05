using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Lambdy.Performance.Benchmarks.Benchmarks.SimpleAliasDapperVsLambdy;
using Lambdy.Performance.Benchmarks.Benchmarks.SimpleAliasQuery;

namespace Lambdy.Performance.Benchmarks
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            RunBenchmarks();
        }

        private static void RunBenchmarks()
        {
            var summary = BenchmarkRunner.Run<SimpleAliasDapperVsLambdyQueryBenchmark>();
        }

        // ReSharper disable once UnusedMember.Local
        // Reason - if benchmark debug is needed switch method in Main to this
        private static void DebugBenchmarks(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
        }
    }
}