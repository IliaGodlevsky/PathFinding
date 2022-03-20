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
    public sealed class TestVertex : IVertex
    {
        private readonly Lazy<IReadOnlyCollection<IVertex>> neighbours;

        public TestVertex(INeighborhood neighborhood, ICoordinate coordinate)
        {
            this.Initialize();
            Position = coordinate;
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(() => neighborhood.GetNeighboursWithinGraph(this));
        }

        public TestVertex(VertexSerializationInfo info)
            : this(info.Neighbourhood, info.Position)
        {
            this.Initialize(info);
        }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public IReadOnlyCollection<IVertex> Neighbours => neighbours.Value;

        public ICoordinate Position { get; }

        public IGraph Graph { get; }

        public bool Equals(IVertex other)
        {
            return Equals((object)other);
        }

        public override bool Equals(object obj)
        {
            return obj is IVertex vertex && vertex.IsEqual(this);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}