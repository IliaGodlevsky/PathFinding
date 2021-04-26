using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GraphLib.Serialization.Tests.Objects
{
    internal sealed class TestVertex : IVertex
    {
        public TestVertex(ICoordinateRadar radar, ICoordinate coorndinate)
        {
            this.Initialize();
            Position = coorndinate;
            CoordinateRadar = radar;
        }

        public TestVertex(VertexSerializationInfo info, ICoordinateRadar radar) :
            this(radar, info.Position)
        {
            this.Initialize(info);
        }

        public bool IsObstacle { get; set; }
        public IVertexCost Cost { get; set; }
        public ICollection<IVertex> Neighbours { get; set; }
        public ICoordinate Position { get; }
        public ICoordinateRadar CoordinateRadar { get; }

        public bool Equals([AllowNull] IVertex other)
        {
            return other.IsEqual(this);
        }
    }
}
