using GraphLib.Base;
using GraphLib.Extensions;
using System.Linq;

namespace GraphLib.Graphs
{
    public sealed class Graph3D : BaseGraph
    {
        public int Width => DimensionsSizes.First();

        public int Length => DimensionsSizes.ElementAt(1);

        public int Height => DimensionsSizes.Last();

        public Graph3D(params int[] dimensions)
            : base(numberOfDimensions: 3, dimensions)
        {

        }
    }
}
