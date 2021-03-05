using System;
using System.Collections.Generic;
using System.Diagnostics;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Lambdy.Performance.Benchmarks.Benchmarks.IterativeVsRecursive;
using Lambdy.Performance.Benchmarks.Benchmarks.SimpleAliasDapperVsLambdy;
using Lambdy.Performance.Benchmarks.Benchmarks.StringConcat;
using Lambdy.Performance.Benchmarks.EfCoreContext;

namespace Lambdy.Performance.Benchmarks
{
    internal static class Program
    {
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            RunBenchmarks();
        }
        
        private static void RunBenchmarks()
        {
            // ReSharper disable once UnusedVariable
            var summary = BenchmarkRunner.Run<IterativeVsRecursiveBenchmark>();
        }

        // ReSharper disable once UnusedMember.Local
        // Reason - if benchmark debug is needed switch method in Main to this
        private static void DebugBenchmarks(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
        }
    }
}