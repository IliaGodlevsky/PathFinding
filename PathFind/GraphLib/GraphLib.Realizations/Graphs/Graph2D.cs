using GraphLib.Base;
using System.Linq;

namespace GraphLib.Realizations.Graphs
{
    public sealed class Graph2D : BaseGraph
    {
        public int Width => DimensionsSizes.First();

        public int Length => DimensionsSizes.Last();

        public Graph2D(params int[] dimensions)
            : base(numberOfDimensions: 2, dimensions)
        {

        }
    }
}