using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators
{
    public sealed class BufferedGraphSerializer<TGraph, TVertex> : IGraphSerializer<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        private readonly IGraphSerializer<TGraph, TVertex> serializer;

        public BufferedGraphSerializer(IGraphSerializer<TGraph, TVertex> serializer)
        {
            this.serializer = serializer;
        }

        public TGraph LoadGraph(Stream stream)
        {
            try
            {
                using (var buffer = new BufferedStream(stream))
                {
                    return serializer.LoadGraph(buffer);
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
                using (var buffer = new BufferedStream(stream))
                {
                    serializer.SaveGraph(graph, buffer);
                }
            }
            catch (Exception ex)
            {
                throw new GraphSerializationException(ex.Message, ex);
            }
        }
    }
}
