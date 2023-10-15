using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.IO;
using System.Text;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class PathfindingHistorySerializer : ISerializer<GraphsPathfindingHistory>
    {
        private readonly ISerializer<GraphPathfindingHistory> historySerializer;
        private readonly ISerializer<IGraph<Vertex>> graphSerializer;

        public PathfindingHistorySerializer(
            ISerializer<GraphPathfindingHistory> historySerializer,
            ISerializer<IGraph<Vertex>> graphSerializer)
        {
            this.historySerializer = historySerializer;
            this.graphSerializer = graphSerializer;
        }

        public GraphsPathfindingHistory DeserializeFrom(Stream stream)
        {
            try
            {
                var history = new GraphsPathfindingHistory();
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

        public void SerializeTo(GraphsPathfindingHistory graphHistory, Stream stream)
        {
            try
            {
                using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
                {
                    writer.Write(graphHistory.Count);
                    foreach (var (graph, history) in graphHistory)
                    {
                        graphSerializer.SerializeTo(graph, stream);
                        historySerializer.SerializeTo(history, stream);
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
