using GraphLib.Graphs.Abstractions;
using System;
using System.Linq;

namespace GraphLib.Graphs
{
    public sealed class Graph2D : BaseGraph
    {
        public int Width => DimensionsSizes.First();

        public int Length => DimensionsSizes.Last();

        public Graph2D(int width, int length)
            : this(new int[] { width, length })
        {

        }

        public Graph2D(params int[] dimensions)
            : base(dimensions)
        {
            if (dimensions.Length != 2)
            {
                throw new ArgumentException("Number of dimensions doesn't match");
            }
        }

        public override string GetFormattedData(string format)
        {
            return string.Format(format, Width, Length,
               ObstaclePercent, ObstacleNumber, Size);
        }
    }
}