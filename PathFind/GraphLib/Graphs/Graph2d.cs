using System;
using GraphLib.Graphs.Abstractions;
using System.Linq;

namespace GraphLib.Graphs
{
    /// <summary>
    /// A structure amounting to a set of objects in which 
    /// some pairs of the objects are in some sense "related"
    /// </summary>
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