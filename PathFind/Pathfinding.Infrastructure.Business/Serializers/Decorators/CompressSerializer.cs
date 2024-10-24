using Pathfinding.Infrastructure.Business.Serializers.Exceptions;
using Pathfinding.Service.Interface;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Serializers.Decorators
{
    public sealed class CompressSerializer<T> : ISerializer<T>
    {
        private readonly ISerializer<T> serializer;

        public CompressSerializer(ISerializer<T> serializer)
        {
            this.serializer = serializer;
        }

        public async Task<T> DeserializeFromAsync(Stream stream, CancellationToken token = default)
        {
            try
            {
                using var compressionStream = new GZipStream(stream, CompressionMode.Decompress, leaveOpen: true);
                return await serializer.DeserializeFromAsync(compressionStream, token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public async Task SerializeToAsync(T graph, Stream stream, CancellationToken token = default)
        {
            try
            {
                using var compressionStream = new GZipStream(stream, CompressionMode.Compress, leaveOpen: true);
                await serializer.SerializeToAsync(graph, compressionStream, token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }
    }
}