using System.Text;
using BenchmarkDotNet.Attributes;

namespace Lambdy.Performance.Benchmarks.Benchmarks.StringConcat
{
    public class StringConcatBenchmark
    {
        [Params(10, 20, 100, 1000, 10000)]
        // ReSharper disable once IdentifierTypo
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public int Concatinations { get; set; }
        
        [Benchmark()]
        public string Interpolation()
        {
            var str = string.Empty;

            for (var i = 0; i < Concatinations; i++)
            {
                str = $"{str}{i.ToString()}";
            }

            return str;
        }

        [Benchmark()]
        public string StringBuilder()
        {
            var str = string.Empty;
            var builder = new StringBuilder();
            
            for (var i = 0; i < Concatinations; i++)
            {
                builder.Append(i.ToString());
            }
            
            return str;
        }

        [Benchmark(Baseline = true)]
        public string StringConcat()
        {
            var str = string.Empty;

            for (var i = 0; i < Concatinations; i++)
            {
                str += i.ToString();
            }
            
            return str;
        }
    }
}