using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;

namespace GraphLib.TestRealizations.TestObjects
{
    public sealed class TestVertex : IVertex
    {
        public TestVertex(ICoordinateRadar radar, ICoordinate coordinate)
        {
            this.Initialize();
            CoordinateRadar = radar;
            Position = coordinate;
        }

        public TestVertex(VertexSerializationInfo info)
            : this(info.CoordinateRadar, info.Position)
        {
            this.Initialize(info);
        }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public ICollection<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; }

        public ICoordinateRadar CoordinateRadar { get; }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }
    }
}
