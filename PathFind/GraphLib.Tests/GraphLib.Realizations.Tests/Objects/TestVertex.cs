using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphLib.Common;

namespace GraphLib.Realizations.Tests.Objects
{
    internal sealed class TestVertex : IVertex
    {
        public TestVertex()
        {
            this.Initialize();
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
