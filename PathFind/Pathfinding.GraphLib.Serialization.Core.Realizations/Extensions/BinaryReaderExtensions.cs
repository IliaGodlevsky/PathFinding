using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    internal static class BinaryReaderExtensions
    {
        public static GraphSerializationInfo ReadGraph(this BinaryReader reader,
            IVertexCostFactory costFactory, ICoordinateFactory coordinateFactory)
        {
            var dimensionsSizes = reader.ReadIntArray();
            var verticesInfo = reader.ReadVertices(costFactory, coordinateFactory);
            var costRange = reader.ReadRange();
            return new GraphSerializationInfo(dimensionsSizes, verticesInfo, costRange);
        }

        private static VertexSerializationInfo[] ReadVertices(this BinaryReader reader,
            IVertexCostFactory costFactory, ICoordinateFactory coordinateFactory)
        {
            int verticesCount = reader.ReadInt32();
            var vertices = new VertexSerializationInfo[verticesCount];
            for (int i = 0; i < verticesCount; i++)
            {
                bool isObstacle = reader.ReadBoolean();
                var cost = reader.ReadCost(costFactory);
                var neighbourhood = reader.ReadNeighborhood(coordinateFactory);
                var position = reader.ReadCoordinate(coordinateFactory);
                vertices[i] = new VertexSerializationInfo(isObstacle, cost, position, neighbourhood);
            }
            return vertices;
        }

        private static int[] ReadIntArray(this BinaryReader reader)
        {
            int count = reader.ReadInt32();
            var coordinates = new int[count];
            for (int i = 0; i < count; i++)
            {
                coordinates[i] = reader.ReadInt32();
            }
            return coordinates;
        }

        private static IReadOnlyCollection<ICoordinate> ReadNeighborhood(this BinaryReader reader, ICoordinateFactory factory)
        {
            int count = reader.ReadInt32();
            var neighborhood = new ICoordinate[count];
            for (int i = 0; i < count; i++)
            {
                neighborhood[i] = reader.ReadCoordinate(factory);
            }
            return neighborhood;
        }

        private static InclusiveValueRange<int> ReadRange(this BinaryReader reader)
        {
            int upperValueOfCostRange = reader.ReadInt32();
            int lowerValueOfCostRange = reader.ReadInt32();
            return new InclusiveValueRange<int>(upperValueOfCostRange, lowerValueOfCostRange);
        }

        private static IVertexCost ReadCost(this BinaryReader reader, IVertexCostFactory factory)
        {
            int cost = reader.ReadInt32();
            return factory.CreateCost(cost);
        }

        private static ICoordinate ReadCoordinate(this BinaryReader reader, ICoordinateFactory factory)
        {
            var coordinates = reader.ReadIntArray();
            return factory.CreateCoordinate(coordinates);
        }
    }
}
