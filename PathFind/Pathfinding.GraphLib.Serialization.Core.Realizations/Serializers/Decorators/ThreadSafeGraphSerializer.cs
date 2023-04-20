using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators
{
    public sealed class ThreadSafeGraphSerializer<TGraph, TVertex> : IGraphSerializer<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        private readonly object syncRoot = new();
        private readonly IGraphSerializer<TGraph, TVertex> serializer;

        public ThreadSafeGraphSerializer(IGraphSerializer<TGraph, TVertex> serializer)
        {
            this.serializer = serializer;
        }

        public TGraph DeserializeFrom(Stream stream)
        {
            lock (syncRoot)
            {
                try
                {
                    return serializer.DeserializeFrom(stream);
                }
                catch (Exception ex)
                {
                    throw new GraphSerializationException(ex.Message, ex);
                }
            }
        }

        public void SerializeTo(TGraph graph, Stream stream)
        {
            lock (syncRoot)
            {
                try
                {
                    serializer.SerializeTo(graph, stream);
                }
                catch (Exception ex)
                {
                    throw new GraphSerializationException(ex.Message, ex);
                }
            }
        }
    }
}
