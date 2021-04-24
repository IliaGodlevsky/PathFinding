using GraphLib.Base;
using System.Linq;

namespace GraphLib.Realizations.Graphs
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
