using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphLib.TestRealizations.TestObjects
{
    [DebuggerDisplay("{Position.ToString()}")]
    public sealed class TestVertex : IVertex, IEquatable<IVertex>
    {
        public TestVertex(INeighboursCoordinates radar, ICoordinate coordinate)
        {
            this.Initialize();
            NeighboursCoordinates = radar;
            Position = coordinate;
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(this.GetNeighbours);
        }

        public TestVertex(VertexSerializationInfo info)
            : this(info.NeighboursCoordinates, info.Position)
        {
            this.Initialize(info);
        }

        public bool IsObstacle { get; set; }
        public IVertexCost Cost { get; set; }
        public IReadOnlyCollection<IVertex> Neighbours => neighbours.Value;
        public ICoordinate Position { get; }
        public INeighboursCoordinates NeighboursCoordinates { get; }
        public IGraph Graph { get; }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        private readonly Lazy<IReadOnlyCollection<IVertex>> neighbours;
    }
}
