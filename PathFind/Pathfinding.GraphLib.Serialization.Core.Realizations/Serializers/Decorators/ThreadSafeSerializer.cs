using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators
{
    public sealed class ThreadSafeSerializer<T> : ISerializer<T>
    {
        private readonly object syncRoot = new();
        private readonly ISerializer<T> serializer;

        public ThreadSafeSerializer(ISerializer<T> serializer)
        {
            this.serializer = serializer;
        }

        public T DeserializeFrom(Stream stream)
        {
            lock (syncRoot)
            {
                try
                {
                    return serializer.DeserializeFrom(stream);
                }
                catch (Exception ex)
                {
                    throw new SerializationException(ex.Message, ex);
                }
            }
        }

        public void SerializeTo(T graph, Stream stream)
        {
            lock (syncRoot)
            {
                try
                {
                    serializer.SerializeTo(graph, stream);
                }
                catch (Exception ex)
                {
                    throw new SerializationException(ex.Message, ex);
                }
            }
        }
    }
}
