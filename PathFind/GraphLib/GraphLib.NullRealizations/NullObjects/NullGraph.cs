using GraphLib.Interfaces;
using NullObject.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.NullRealizations.NullObjects
{
    /// <summary>
    /// Represents an empty graph, without any vertices
    /// </summary>
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

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public IEnumerable<IVertex> Vertices => vertices;

        public int Size => 1;

        public int ObstaclePercent => 100;

        public int Obstacles => 1;

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

        private readonly NullVertex[] vertices;
    }
}
