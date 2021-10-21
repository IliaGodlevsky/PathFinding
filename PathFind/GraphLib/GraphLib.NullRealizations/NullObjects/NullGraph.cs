using GraphLib.Interfaces;
using NullObject.Attributes;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    public sealed class NullGraph : IGraph
    {
        public NullGraph()
        {
            Vertices = new NullVertex[] { };
            DimensionsSizes = new int[] { };
        }

        public IVertex this[ICoordinate position]
        {
            get => new NullVertex();
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
