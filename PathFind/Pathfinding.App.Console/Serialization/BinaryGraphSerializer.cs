using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System;
using System.IO;
using System.Text;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class BinaryGraphSerializer : ISerializer<GraphSerializationDto>
    {
        public GraphSerializationDto DeserializeFrom(Stream stream)
        {
            try
            {
                using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
                {
                    var dimensions = reader.ReadIntArray();
                    int count = reader.ReadInt32();
                    var vertices = new VertexSerializationDto[count];
                    for (int vertex = 0; vertex < count; vertex++)
                    {
                        vertices[vertex] = new()
                        {
                            Position = reader.ReadCoordinate(),
                            Cost = reader.ReadCost(),
                            IsObstacle = reader.ReadBoolean(),
                            Neighbors = reader.ReadCoordinates()
                        };
                    }
                    return new() { DimensionSizes = dimensions, Vertices = vertices };
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(GraphSerializationDto item, Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                try
                {
                    writer.WriteIntArray(item.DimensionSizes);
                    writer.Write(item.Vertices.Count);
                    foreach (var vertex in item.Vertices)
                    {
                        writer.WriteIntArray(vertex.Position);
                        writer.Write(vertex.Cost.CurrentCost);
                        writer.WriteRange(vertex.Cost.CostRange);
                        writer.Write(vertex.IsObstacle);
                        writer.WriteCoordinates(vertex.Neighbors);
                    }
                }
                catch (Exception ex)
                {
                    throw new SerializationException(ex.Message, ex);
                }
            }
        }
    }
}
