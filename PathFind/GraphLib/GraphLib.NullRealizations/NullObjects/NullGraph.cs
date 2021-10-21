using GraphLib.Interfaces;
using NullObject.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    public sealed class NullGraph : IGraph
    {
        public NullGraph()
        {
            Vertices = new NullVertex[] { new NullVertex() };
            DimensionsSizes = Array.Empty<int>();
        }

        public IVertex this[ICoordinate position]
        {
            get => Vertices.First();
            set => _ = value;
        }

        public int[] DimensionsSizes { get; }

        public IEnumerable<IVertex> Vertices { get; }

        public int Size => 0;

        public override bool Equals(object obj)
        {
            return obj is NullGraph;
        }

        public override string ToString()
        {
            return string.Empty;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IGraph Clone()
        {
            return new NullGraph();
        }
    }
}
