using Common.Attributes;
using GraphLib.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Common.NullObjects
{
    /// <summary>
    /// Respresents an empty graph, without any vertices
    /// </summary>
    [Default]
    public sealed class NullGraph : IGraph
    {
        public NullGraph()
        {

        }

        public IVertex this[ICoordinate position]
        {
            get => new DefaultVertex();
            set => _ = value;
        }

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public IEnumerable<IVertex> Vertices => new DefaultVertex[] { };

        public int Size => 0;

        public int ObstaclePercent => 0;

        public int Obstacles => 0;

        public IVertex this[IEnumerable<int> coordinates]
        {
            get => new DefaultVertex();
            set => _ = value;
        }

        public IVertex this[int index]
        {
            get => new DefaultVertex();
            set => _ = value;
        }

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
    }
}
