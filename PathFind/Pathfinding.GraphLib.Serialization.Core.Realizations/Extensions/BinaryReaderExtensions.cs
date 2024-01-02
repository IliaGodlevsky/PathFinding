using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Primitives.ValueRange;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static GraphSerializationInfo ReadGraph(this BinaryReader reader)
        {
            var dimensionsSizes = reader.ReadIntArray();
            var verticesInfo = reader.ReadVertices();
            return new(dimensionsSizes, verticesInfo);
        }

        private static VertexSerializationInfo[] ReadVertices(this BinaryReader reader)
        {
            int verticesCount = reader.ReadInt32();
            var vertices = new VertexSerializationInfo[verticesCount];
            for (int i = 0; i < verticesCount; i++)
            {
                vertices[i] = reader.ReadVertex();
            }
            return vertices;
        }

        private static VertexSerializationInfo ReadVertex(this BinaryReader reader)
        {
            bool isObstacle = reader.ReadBoolean();
            var cost = reader.ReadCost();
            var neighbourhood = reader.ReadCoordinates();
            var position = reader.ReadCoordinate();
            return new(isObstacle, cost, position, neighbourhood);
        }

        public static int[] ReadIntArray(this BinaryReader reader)
        {
            int count = reader.ReadInt32();
            var coordinates = new int[count];
            for (int i = 0; i < count; i++)
            {
                coordinates[i] = reader.ReadInt32();
            }
            return coordinates;
        }

        private static InclusiveValueRange<int> ReadRange(this BinaryReader reader)
        {
            int upperValueOfCostRange = reader.ReadInt32();
            int lowerValueOfCostRange = reader.ReadInt32();
            return new(upperValueOfCostRange, lowerValueOfCostRange);
        }

        public static ICoordinate[] ReadCoordinates(this BinaryReader reader)
        {
            int count = reader.ReadInt32();
            var neighborhood = new ICoordinate[count];
            for (int i = 0; i < count; i++)
            {
                neighborhood[i] = reader.ReadCoordinate();
            }
            return neighborhood;
        }

        public static IVertexCost ReadCost(this BinaryReader reader)
        {
            int cost = reader.ReadInt32();
            var range = reader.ReadRange();
            return new VertexCost(cost, range);
        }

        public static ICoordinate ReadCoordinate(this BinaryReader reader)
        {
            var coordinates = reader.ReadIntArray();
            return new Coordinate(coordinates);
        }
    }
}
