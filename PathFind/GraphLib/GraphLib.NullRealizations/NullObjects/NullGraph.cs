using GraphLib.Interfaces;
using NullObject.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    public sealed class NullGraph : IGraph
    {
        public NullGraph()
        {
            vertices = new NullVertex[] { new NullVertex() };
        }

        public IVertex this[ICoordinate position]
        {
            get => vertices.First();
            set => _ = value;
        }

        public int[] DimensionsSizes => new int[] { };

        public IEnumerable<IVertex> Vertices => vertices;

        public int Size => vertices.Length;

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

        private readonly NullVertex[] vertices;
    }
}
