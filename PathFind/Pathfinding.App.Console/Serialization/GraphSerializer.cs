using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class GraphSerializer : ISerializer<SerializationInfo>
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> rangeSerializer;
        private readonly ISerializer<IPathfindingHistory> historySerializer;
        private readonly ISerializer<Graph2D<Vertex>> graphSerializer;

        public GraphSerializer(ISerializer<IEnumerable<ICoordinate>> rangeSerializer,
            ISerializer<IPathfindingHistory> historySerializer,
            ISerializer<Graph2D<Vertex>> graphSerializer)
        {
            this.rangeSerializer = rangeSerializer;
            this.historySerializer = historySerializer;
            this.graphSerializer = graphSerializer;
        }

        public SerializationInfo DeserializeFrom(Stream stream)
        {
            try
            {
                var graph = graphSerializer.DeserializeFrom(stream);
                var range = rangeSerializer.DeserializeFrom(stream);
                var unit = historySerializer.DeserializeFrom(stream);
                return new SerializationInfo { Graph = graph, Range = range, History = unit };
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
                historySerializer.SerializeTo(item.History, stream);
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }
    }
}
