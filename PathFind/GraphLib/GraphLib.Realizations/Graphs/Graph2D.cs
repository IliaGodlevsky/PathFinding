using GraphLib.Base;
using GraphLib.Interfaces;
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

        public override IGraph Clone()
        {
            var graph = new Graph2D(DimensionsSizes);
            foreach(var vertex in Vertices)
            {
                var temp = vertex.Clone();
                graph[temp.Position] = temp;
            }
            return graph;
        }
    }
}