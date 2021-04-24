using GraphLib.Common;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GraphLib.Serialization.Tests.Objects
{
    internal sealed class TestVertex : IVertex
    {
        public TestVertex()
        {
            this.Initialize();
        }

        public TestVertex(VertexSerializationInfo info) : this()
        {
            this.Initialize(info);
        }

        public bool IsObstacle { get; set; }
        public IVertexCost Cost { get; set; }
        public ICollection<IVertex> Neighbours { get; set; }
        public ICoordinate Position { get; set; }
        public ICoordinateRadar CoordinateRadar => new CoordinateAroundRadar(Position);

        public bool Equals([AllowNull] IVertex other)
        {
            return other.IsEqual(this);
        }
    }
}
