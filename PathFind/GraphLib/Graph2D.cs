using GraphLib.Base;
using GraphLib.Extensions;
using System;
using System.Linq;

namespace GraphLib.Graphs
{
    public sealed class Graph2D : BaseGraph
    {
        public int Width => DimensionsSizes.First();

        public int Length => DimensionsSizes.Last();

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
               this.GetObstaclesPercent(), this.GetObstaclesCount(), this.GetSize());
        }
    }
}