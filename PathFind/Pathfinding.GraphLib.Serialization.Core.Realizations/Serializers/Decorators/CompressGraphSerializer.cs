using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;
using System.IO.Compression;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators
{
    public sealed class CompressGraphSerializer<TGraph, TVertex> : IGraphSerializer<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        private readonly IGraphSerializer<TGraph, TVertex> serializer;

        public CompressGraphSerializer(IGraphSerializer<TGraph, TVertex> serializer)
        {
            this.serializer = serializer;
        }

        public TGraph LoadGraph(Stream stream)
        {
            try
            {
                using (var compressionStream = new GZipStream(stream, CompressionMode.Decompress, leaveOpen: true))
                {
                    return serializer.LoadGraph(compressionStream);
                }
            }
            catch (Exception ex)
            {
                throw new GraphSerializationException(ex.Message, ex);
            }
        }

        public void SaveGraph(IGraph<IVertex> graph, Stream stream)
        {
            try
            {
                using (var compressionStream = new GZipStream(stream, CompressionMode.Compress, leaveOpen: true))
                {
                    serializer.SaveGraph(graph, compressionStream);
                }
            }
            catch (Exception ex)
            {
                throw new GraphSerializationException(ex.Message, ex);
            }
        }
    }
}