using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class NeighborhoodLayer : ILayer
    {
        private readonly INeighborhoodFactory factory;
        private readonly ReturnOptions options;

        public NeighborhoodLayer(INeighborhoodFactory factory, ReturnOptions options = null)
        {
            this.factory = factory;
            this.options = options ?? ReturnOptions.Limit;
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
            return self.Select(x => x.CoordinatesValues.Select(ReturnInRange))
                .Select(x => new Coordinate(x.ToArray()))
                .Distinct()
                .Select(graph.Get)
                .ToReadOnly();
        }
    }
}
