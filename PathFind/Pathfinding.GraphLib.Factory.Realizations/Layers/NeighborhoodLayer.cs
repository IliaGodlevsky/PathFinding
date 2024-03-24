using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Factory.Realizations.Layers
{
    public sealed class NeighborhoodLayer : ILayer
    {
        private readonly INeighborhoodFactory factory;
        private readonly ReturnOptions options;

        public NeighborhoodLayer(INeighborhoodFactory factory, ReturnOptions options)
        {
            this.factory = factory;
            this.options = options;
        }

        public NeighborhoodLayer(INeighborhoodFactory factory)
            : this(factory, ReturnOptions.Limit)
        {

        }

        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (var vertex in graph)
            {
                var neighborhood = factory.CreateNeighborhood(vertex.Position);
                var neighbours = GetNeighboursWithinGraph(neighborhood, graph);
                vertex.Neighbours.AddRange(neighbours);
            }
        }

        private IReadOnlyCollection<IVertex> GetNeighboursWithinGraph(INeighborhood self,
            IGraph<IVertex> graph)
        {
            int ReturnInRange(int coordinate, int index)
            {
                var range = new InclusiveValueRange<int>(graph.DimensionsSizes[index] - 1);
                return range.ReturnInRange(coordinate, options);
            }
            return self.Select(x => x.Select(ReturnInRange))
                .Select(x => new Coordinate(x.ToArray()))
                .Distinct()
                .Select(graph.Get)
                .ToReadOnly();
        }
    }
}
