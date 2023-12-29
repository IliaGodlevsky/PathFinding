using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class PathfindingHistorySerializer : ISerializer<IEnumerable<PathfindingHistorySerializationDto>>
    {
        private readonly ISerializer<IGraph<Vertex>> graphSerializer;
        private readonly ISerializer<IEnumerable<AlgorithmSerializationDto>> historySerializer;
        private readonly ISerializer<IEnumerable<ICoordinate>> rangeSerializer;

        public PathfindingHistorySerializer(
            ISerializer<IEnumerable<AlgorithmSerializationDto>> historySerializer,
            ISerializer<IGraph<Vertex>> graphSerializer,
            ISerializer<IEnumerable<ICoordinate>> rangeSerializer)
        {
            this.historySerializer = historySerializer;
            this.graphSerializer = graphSerializer;
            this.rangeSerializer = rangeSerializer;
        }

        public IEnumerable<PathfindingHistorySerializationDto> DeserializeFrom(Stream stream)
        {
            try
            {
                using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
                {
                    int count = reader.ReadInt32();
                    var histories = new PathfindingHistorySerializationDto[count];
                    for (int history = 0; history < count; history++)
                    {
                        histories[history] = new()
                        {
                            Graph = graphSerializer.DeserializeFrom(stream),
                            Algorithms = historySerializer.DeserializeFrom(stream).ToReadOnly(),
                            Range = rangeSerializer.DeserializeFrom(stream).ToReadOnly()
                        };
                    }
                    return Array.AsReadOnly(histories);
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(IEnumerable<PathfindingHistorySerializationDto> graphHistory, 
            Stream stream)
        {
            try
            {
                using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
                {
                    var histories = graphHistory.ToArray();
                    writer.Write(histories.Length);
                    foreach (var history in histories)
                    {
                        graphSerializer.SerializeTo(history.Graph, stream);
                        historySerializer.SerializeTo(history.Algorithms, stream);
                        rangeSerializer.SerializeTo(history.Range, stream);
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
