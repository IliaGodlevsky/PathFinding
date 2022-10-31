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
        public TestVertex(ICoordinate coordinate)
        {
            this.Initialize();
            Position = coordinate;
        }

        public TestVertex(VertexSerializationInfo info)
            : this(info.Position)
        {
            this.Initialize(info);
        }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public IReadOnlyCollection<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        public override bool Equals(object obj)
        {
            return obj is IVertex vertex && vertex.IsEqual(this);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cost.CurrentCost, Position.GetHashCode());
        }
    }
}