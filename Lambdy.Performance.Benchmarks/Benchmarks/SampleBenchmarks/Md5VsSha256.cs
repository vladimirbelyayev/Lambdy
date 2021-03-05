using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;

namespace Lambdy.Performance.Benchmarks.Benchmarks.SampleBenchmarks
{
    // [SimpleJob(RuntimeMoniker.Net472, baseline: true)]
    // [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    public class Md5VsSha256
    {
        private const int N = 10000;
        private readonly byte[] _data;

        private readonly SHA256 _sha256 = SHA256.Create();
        private readonly MD5 _md5 = MD5.Create();

        public Md5VsSha256()
        {
            _data = new byte[N];
            new Random(42).NextBytes(_data);
        }
        
        [Benchmark(Baseline = true)]
        public byte[] Sha256() => _sha256.ComputeHash(_data);

        [Benchmark]
        public byte[] Md5() => _md5.ComputeHash(_data);
    }
}