using Common.Interface;
using GraphLib.Interfaces;
using NullObject.Attributes;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    public sealed class NullVertex : IVertex, IEquatable<IVertex>, ICloneable<IVertex>
    {
        public NullVertex()
        {
            neighbours = new NullVertex[] { };
        }

        public bool IsObstacle { get => true; set { } }

        public IVertexCost Cost { get => new NullCost(); set { } }

        public IReadOnlyCollection<IVertex> Neighbours { get => neighbours; set { } }

        public ICoordinate Position { get => new NullCoordinate(); set { } }

        public bool Equals(IVertex other) => other is NullVertex;

        public IVertex Clone()
        {
            return new NullVertex();
        }

        public INeighboursCoordinates NeighboursCoordinates => new NullNeighboursCoordinates();

        private readonly IVertex[] neighbours;
    }
}