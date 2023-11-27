using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class PathfindingHistorySerializer : ISerializer<GraphsPathfindingHistory>
    {
        private readonly ISerializer<IGraph<Vertex>> graphSerializer;
        private readonly ISerializer<GraphPathfindingHistory> historySerializer;
        private readonly ISerializer<IEnumerable<ICoordinate>> rangeSerializer;

        public PathfindingHistorySerializer(
            ISerializer<GraphPathfindingHistory> historySerializer,
            ISerializer<IGraph<Vertex>> graphSerializer,
            ISerializer<IEnumerable<ICoordinate>> rangeSerializer)
        {
            this.historySerializer = historySerializer;
            this.graphSerializer = graphSerializer;
            this.rangeSerializer = rangeSerializer;
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
                        int key = history.Add(graph);
                        var smooth = reader.ReadSmoothHistory()
                            .Reverse()
                            .ForEach(history.GetSmoothHistory(key).Push);
                        var range = rangeSerializer.DeserializeFrom(stream);
                        var pathfindingRange = history.GetRange(key);
                        pathfindingRange.AddRange(range);
                        var graphHistory = historySerializer.DeserializeFrom(stream);
                        history.Add(key, graphHistory);
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
                    foreach (int id in graphHistory.Ids)
                    {
                        var graph = graphHistory.GetGraph(id);
                        graphSerializer.SerializeTo(graph, stream);
                        var smooth = graphHistory.GetSmoothHistory(id);
                        writer.Write(smooth.Count);
                        smooth.ForEach(writer.WriteIntArray);
                        var range = graphHistory.GetRange(id);
                        rangeSerializer.SerializeTo(range, stream);
                        var history = graphHistory.GetHistory(id);
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
