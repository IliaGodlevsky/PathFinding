using GraphLib.Base;
using System.Linq;

namespace GraphLib.Realizations.Graphs
{
    public sealed class Graph2D : BaseGraph
    {
        public int Width { get; }

        public int Length { get; }

        public Graph2D(params int[] dimensions)
            : base(numberOfDimensions: 2, dimensions)
        {
            Width = DimensionsSizes.First();
            Length = DimensionsSizes.Last();
        }
    }
}