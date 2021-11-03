using GraphLib.Base;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Graphs
{
    public sealed class Graph2D : BaseGraph, IGraph
    {
        public int Width { get; }

        public int Length { get; }

        public Graph2D(IEnumerable<IVertex> vertices, params int[] dimensions)
            : base(requiredNumberOfDimensions: 2, vertices, dimensions)
        {
            Width = DimensionsSizes.First();
            Length = DimensionsSizes.Last();
        }
    }
}