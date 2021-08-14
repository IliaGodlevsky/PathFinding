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

        public IReadOnlyCollection<IVertex> Neighbours { get => neighbours; set { } }

        public ICoordinate Position { get => new NullCoordinate(); set { } }

        public bool Equals(IVertex other) => other is NullVertex;

        public INeighboursCoordinates NeighboursCoordinates => new NullNeighboursCoordinates();

        private readonly List<IVertex> neighbours;
    }
}