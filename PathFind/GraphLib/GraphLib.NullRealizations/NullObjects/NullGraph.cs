using GraphLib.Interfaces;
using NullObject.Attributes;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    public sealed class NullGraph : IGraph
    {
        public static IGraph Instance => instance.Value;

        public IVertex this[ICoordinate position]
        {
            get => NullVertex.Instance;
        }

        public int[] DimensionsSizes { get; }

        public IReadOnlyCollection<IVertex> Vertices => new IVertex[] { NullVertex.Instance };

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
            return NullGraph.Instance;
        }

        private NullGraph()
        {
            DimensionsSizes = Array.Empty<int>();
        }

        private static readonly Lazy<IGraph> instance = new Lazy<IGraph>(() => new NullGraph());
    }
}
