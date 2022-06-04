using GraphLib.Interfaces;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Interfaces;
using System;
using System.IO;

namespace GraphLib.Serialization.Serializers.Decorators
{
    public sealed class ThreadSafeGraphSerializer : IGraphSerializer
    {
        private readonly object locker;
        private readonly IGraphSerializer serializer;

        public ThreadSafeGraphSerializer(IGraphSerializer serializer)
        {
            this.serializer = serializer;
            locker = new object();
        }

        public IGraph LoadGraph(Stream stream)
        {
            lock (locker)
            {
                try
                {
                    return serializer.LoadGraph(stream);
                }
                catch (Exception ex)
                {
                    throw new CantSerializeGraphException(ex.Message, ex);
                }
            }
        }

        public void SaveGraph(IGraph graph, Stream stream)
        {
            lock (locker)
            {
                try
                {
                    serializer.SaveGraph(graph, stream);
                }
                catch (Exception ex)
                {
                    throw new CantSerializeGraphException(ex.Message, ex);
                }
            }
        }
    }
}
