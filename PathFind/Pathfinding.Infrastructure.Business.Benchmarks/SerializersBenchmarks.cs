using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using Pathfinding.Infrastructure.Business.Benchmarks.Data;
using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Infrastructure.Business.Serializers.Decorators;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Benchmarks
{
    [MemoryDiagnoser]
    public class SerializersBenchmarks
    {
        private static Dictionary<string, object> toSerialize;

        [GlobalSetup]
        public void Setup()
        {
            toSerialize = SerializationDataProvider.Data;
        }

        [Benchmark]
        public async Task CompressingSerializerBenchmark()
        {
            var serializer = new JsonSerializer<Dictionary<string, object>>();
            var compress = new CompressSerializer<Dictionary<string, object>>(serializer);
            var memory = new MemoryStream();
            await compress.SerializeToAsync(toSerialize, memory);
        }

        [Benchmark(Baseline = true)]
        public async Task RegularSerializerBenchmark()
        {
            var serializer = new JsonSerializer<Dictionary<string, object>>();
            var memory = new MemoryStream();
            await serializer.SerializeToAsync(toSerialize, memory);
        }

        [Benchmark]
        public async Task CryptoSerializerBenchmark()
        {
            var serializer = new JsonSerializer<Dictionary<string, object>>();
            var crypto = new CryptoSerializer<Dictionary<string, object>>(serializer);
            var memory = new MemoryStream();
            await serializer.SerializeToAsync(toSerialize, memory);
        }

        [Benchmark]
        public async Task CryptoCompressSerializerBenchmark()
        {
            var serializer = new JsonSerializer<Dictionary<string, object>>();
            var compress = new CompressSerializer<Dictionary<string, object>>(serializer);
            var crypto = new CryptoSerializer<Dictionary<string, object>>(compress);
            var memory = new MemoryStream();
            await crypto.SerializeToAsync(toSerialize, memory);
        }
    }
}
