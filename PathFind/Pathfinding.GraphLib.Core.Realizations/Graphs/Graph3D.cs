using Pathfinding.GraphLib.Core.Abstractions;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Graphs
{
    public sealed class Graph3D<TVertex> : Graph<TVertex>
        where TVertex : IVertex
    {
        public static readonly Graph3D<TVertex> Empty
            = new (Array.Empty<TVertex>(), Array.Empty<int>());

        public int Width { get; }

        public int Length { get; }

        public int Height { get; }

        public Graph3D(IReadOnlyCollection<TVertex> vertices, IReadOnlyList<int> dimensions)
            : base(requiredNumberOfDimensions: 3, vertices, dimensions)
        {
            Width = DimensionsSizes.FirstOrDefault();
            Length = DimensionsSizes.ElementAtOrDefault(1);
            Height = DimensionsSizes.LastOrDefault();
        }
    }
}
