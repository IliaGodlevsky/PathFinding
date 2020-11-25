using GraphLib.Graphs.Abstractions;
using System;
using System.Linq;

namespace GraphLib.Graphs
{
    public sealed class Graph4D : BaseGraph
    {
        public int Width => DimensionsSizes.First();

        public int Length => DimensionsSizes.ElementAt(1);

        public int Height => DimensionsSizes.ElementAt(2);

        public int FourthDimension => DimensionsSizes.Last();

        public Graph4D(int width, int lenght, int height, int fourth)
            : this(new int[] { width, lenght, height, fourth })
        {

        }

        public Graph4D(params int[] dimensions) : base(dimensions)
        {
            if (dimensions.Length != 4)
            {
                throw new ArgumentException("Number of dimensions doesn't match");
            }
        }

        public override string GetFormattedData(string format)
        {
            return string.Empty;
        }
    }
}