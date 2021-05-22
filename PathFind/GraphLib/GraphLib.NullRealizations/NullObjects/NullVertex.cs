using GraphLib.Interfaces;
using NullObject.Attributes;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    public sealed class NullVertex : IVertex
    {
        public NullVertex()
        {
            neighbours = new List<IVertex> { this };
        }

        public bool IsObstacle { get => true; set { } }

        public IVertexCost Cost { get => new NullCost(); set { } }

        public ICollection<IVertex> Neighbours { get => neighbours; set { } }

        public ICoordinate Position { get => new NullCoordinate(); set { } }

        public bool Equals(IVertex other) => other is NullVertex;

        public ICoordinateRadar CoordinateRadar => new NullCoordinateRadar();

        private readonly List<IVertex> neighbours;
    }
}