using Common.Attributes;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Common.NullObjects
{
    [Null]
    public sealed class NullVertex : IVertex
    {
        public bool IsObstacle { get => true; set { } }

        public IVertexCost Cost { get => new NullCost(); set { } }

        public ICollection<IVertex> Neighbours { get => new List<IVertex>(); set { } }

        public ICoordinate Position { get => new NullCoordinate(); set { } }

        public bool Equals(IVertex other) => other is NullVertex;

        public ICoordinateRadar CoordinateRadar => new NullCoordinateRadar();
    }
}