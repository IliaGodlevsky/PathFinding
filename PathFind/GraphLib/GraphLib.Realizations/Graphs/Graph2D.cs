using GraphLib.Base;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Graphs
{
    public sealed class Graph2D : Graph
    {
        public int Width { get; }

        public int Length { get; }

        public Graph2D(IReadOnlyCollection<IVertex> vertices, IReadOnlyList<int> dimensions)
            : base(requiredNumberOfDimensions: 2, vertices, dimensions)
        {
            Width = DimensionsSizes.First();
            Length = DimensionsSizes.Last();
        }
    }
}