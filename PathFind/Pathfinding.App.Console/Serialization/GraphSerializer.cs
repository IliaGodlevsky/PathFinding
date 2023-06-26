using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;
using System.Text;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class GraphSerializer : ISerializer<PathfindingHistory>
    {
        private readonly ISerializer<GraphPathfindingHistory> historySerializer;
        private readonly ISerializer<Graph2D<Vertex>> graphSerializer;

        public GraphSerializer(ISerializer<GraphPathfindingHistory> historySerializer,
            ISerializer<Graph2D<Vertex>> graphSerializer)
        {
            this.historySerializer = historySerializer;
            this.graphSerializer = graphSerializer;
        }

        public PathfindingHistory DeserializeFrom(Stream stream)
        {
            try
            {
                var history = new PathfindingHistory();
                using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
                {
                    int count = reader.ReadInt32();
                    while (count-- > 0)
                    {
                        var graph = graphSerializer.DeserializeFrom(stream);
                        var graphHistory = historySerializer.DeserializeFrom(stream);
                        history.Add(graph, graphHistory);
                    }
                }
                return history;
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(PathfindingHistory item, Stream stream)
        {
            try
            {
                using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
                {
                    writer.Write(item.Count);
                    foreach (var note in item)
                    {
                        graphSerializer.SerializeTo(note.Graph, stream);
                        historySerializer.SerializeTo(note.History, stream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }
    }
}
