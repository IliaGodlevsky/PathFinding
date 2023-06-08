using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;
using System.IO.Compression;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators
{
    public sealed class CompressSerializer<T> : ISerializer<T>
    {
        private readonly ISerializer<T> serializer;

        public CompressSerializer(ISerializer<T> serializer)
        {
            this.serializer = serializer;
        }

        public T DeserializeFrom(Stream stream)
        {
            try
            {
                using (var compressionStream = new GZipStream(stream, CompressionMode.Decompress, leaveOpen: true))
                {
                    return serializer.DeserializeFrom(compressionStream);
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(T graph, Stream stream)
        {
            try
            {
                using (var compressionStream = new GZipStream(stream, CompressionMode.Compress, leaveOpen: true))
                {
                    serializer.SerializeTo(graph, compressionStream);
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }
    }
}