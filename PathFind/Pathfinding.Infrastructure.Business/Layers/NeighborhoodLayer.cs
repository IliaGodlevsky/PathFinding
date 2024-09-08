using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
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

        public NeighborhoodLayer(INeighborhoodFactory factory, ReturnOptions options)
        {
            this.factory = factory ?? new MooreNeighborhoodFactory();
            this.options = options ?? ReturnOptions.Limit;
        }

        public NeighborhoodLayer()
            : this(new MooreNeighborhoodFactory(), ReturnOptions.Limit)
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
            bool IsInRange(Coordinate coordinate)
            {
                return coordinate.CoordinatesValues
                    .Juxtapose(graph.DimensionsSizes, (x, y) => x < y && x >= 0);

            }
            var selected = self.Where(IsInRange).ToArray();
            var distinct = selected.Distinct().ToList();
            var vertices = distinct.Select(graph.Get).ToList();
            var values = vertices.ToReadOnly();
            return values;
        }
    }
}
