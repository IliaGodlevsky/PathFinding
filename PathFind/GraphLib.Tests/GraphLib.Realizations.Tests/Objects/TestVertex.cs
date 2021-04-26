using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GraphLib.Realizations.Tests.Objects
{
    internal sealed class TestVertex : IVertex
    {
        public TestVertex(ICoordinateRadar radar, ICoordinate coordinate)
        {
            this.Initialize();
            CoordinateRadar = radar;
            Position = coordinate;
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
