using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class GraphSerializer : ISerializer<SerializationInfo>
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> rangeSerializer;
        private readonly ISerializer<Graph2D<Vertex>> graphSerializer;
        private readonly ICoordinateFactory coordinateFactory;

        public GraphSerializer(ISerializer<IEnumerable<ICoordinate>> rangeSerializer,
            ISerializer<Graph2D<Vertex>> graphSerializer,
            ICoordinateFactory coordinateFactory)
        {
            this.rangeSerializer = rangeSerializer;
            this.graphSerializer = graphSerializer;
            this.coordinateFactory = coordinateFactory;
        }

        public SerializationInfo DeserializeFrom(Stream stream)
        {
            try
            {
                var graph = graphSerializer.DeserializeFrom(stream);
                var range = rangeSerializer.DeserializeFrom(stream);
                var info =  new SerializationInfo { Graph = graph, Range = range.ToArray() };
                using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
                {
                    return reader.ReadHistory(info, coordinateFactory);
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(SerializationInfo item, Stream stream)
        {
            try
            {
                graphSerializer.SerializeTo(item.Graph, stream);
                rangeSerializer.SerializeTo(item.Range, stream);
                using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
                {
                    writer.WriteHistory(item);
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }
    }
}
