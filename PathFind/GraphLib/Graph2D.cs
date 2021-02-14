using GraphLib.Base;
using GraphLib.Extensions;
using System.Linq;

namespace GraphLib.Graphs
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