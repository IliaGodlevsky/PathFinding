using Pathfinding.GraphLib.Core.Abstractions;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Graphs
{
    public class Graph2D<TVertex> : Graph<TVertex>
        where TVertex : IVertex
    {
        public static readonly Graph2D<TVertex> Empty
            = new(Array.Empty<TVertex>(), Array.Empty<int>());

        public int Width { get; }

        public int Length { get; }

        public Graph2D(IReadOnlyCollection<TVertex> vertices, IReadOnlyList<int> dimensions)
            : base(requiredNumberOfDimensions: 2, vertices, dimensions)
        {
            Width = DimensionsSizes.FirstOrDefault();
            Length = DimensionsSizes.LastOrDefault();
        }

        public Graph2D(Graph2D<TVertex> graph)
            : this(graph, graph.DimensionsSizes)
        {

        }
    }
}