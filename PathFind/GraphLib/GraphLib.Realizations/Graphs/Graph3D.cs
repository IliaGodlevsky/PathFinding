using GraphLib.Base;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Graphs
{
    public sealed class Graph3D : BaseGraph, IGraph
    {
        public int Width { get; }

        public int Length { get; }

        public int Height { get; }

        public Graph3D(IEnumerable<IVertex> vertices, params int[] dimensions)
            : base(numberOfDimensions: 3, vertices, dimensions)
        {
            Width = DimensionsSizes.First();
            Length = DimensionsSizes.ElementAt(1);
            Height = DimensionsSizes.Last();
        }
    }
}
