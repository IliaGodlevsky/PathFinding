using GraphLib.Graphs.Abstractions;
using System;
using System.Linq;

namespace GraphLib.Graphs
{
    public sealed class Graph3D : BaseGraph
    {
        public int Width => DimensionsSizes.First();

        public int Length => DimensionsSizes.ElementAt(1);

        public int Height => DimensionsSizes.Last();

        public Graph3D(params int[] dimensions) : base(dimensions)
        {
            if (dimensions.Length != 3)
            {
                throw new ArgumentException("Number of dimensions doesn't match");
            }
        }

        public override string GetFormattedData(string format)
        {
            return string.Format(format, Width, Length, Height,
                ObstaclePercent, ObstacleNumber, Size);
        }
    }
}
