using Common.Interface;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Realizations.Graphs
{
    public sealed class Graph3D : BaseGraph, IGraph, ICloneable<IGraph>
    {
        public int Width { get; }

        public int Length { get; }

        public int Height { get; }

        public Graph3D(params int[] dimensions)
            : base(numberOfDimensions: 3, dimensions)
        {
            Width = DimensionsSizes.First();
            Length = DimensionsSizes.ElementAt(1);
            Height = DimensionsSizes.Last();
        }

        public override IGraph Clone()
        {
            var graph = new Graph3D(DimensionsSizes);
            return graph.CloneVertices(this);
        }
    }
}
