using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;

namespace GraphLib.TestRealizations.TestObjects
{
    public sealed class TestVertex : IVertex
    {
        public TestVertex(INeighboursCoordinates radar, ICoordinate coordinate)
        {
            this.Initialize();
            NeighboursCoordinates = radar;
            Position = coordinate;
        }

        public TestVertex(VertexSerializationInfo info)
            : this(info.NeighboursCoordinates, info.Position)
        {
            this.Initialize(info);
        }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public IReadOnlyCollection<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; }

        public INeighboursCoordinates NeighboursCoordinates { get; }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }
    }
}
