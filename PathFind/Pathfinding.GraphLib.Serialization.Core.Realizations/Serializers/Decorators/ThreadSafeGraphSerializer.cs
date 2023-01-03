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
        private readonly object locker;
        private readonly IGraphSerializer<TGraph, TVertex> serializer;

        public ThreadSafeGraphSerializer(IGraphSerializer<TGraph, TVertex> serializer)
        {
            this.serializer = serializer;
            locker = new object();
        }

        public TGraph LoadGraph(Stream stream)
        {
            lock (locker)
            {
                try
                {
                    return serializer.LoadGraph(stream);
                }
                catch (Exception ex)
                {
                    throw new GraphSerializationException(ex.Message, ex);
                }
            }
        }

        public void SaveGraph(IGraph<IVertex> graph, Stream stream)
        {
            lock (locker)
            {
                try
                {
                    serializer.SaveGraph(graph, stream);
                }
                catch (Exception ex)
                {
                    throw new GraphSerializationException(ex.Message, ex);
                }
            }
        }
    }
}
