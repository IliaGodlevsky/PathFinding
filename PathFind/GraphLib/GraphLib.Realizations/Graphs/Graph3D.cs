using GraphLib.Base;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Graphs
{
    public sealed class Graph3D : BaseGraph
    {
        public int Width { get; }

        public int Length { get; }

        public int Height { get; }

        public Graph3D(IReadOnlyCollection<IVertex> vertices, IReadOnlyList<int> dimensions)
            : base(requiredNumberOfDimensions: 3, vertices, dimensions)
        {
            Width = DimensionsSizes.First();
            Length = DimensionsSizes.ElementAt(1);
            Height = DimensionsSizes.Last();
        }
    }
}
