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

        public NeighborhoodLayer(INeighborhoodFactory factory)
        {
            this.factory = factory ?? new MooreNeighborhoodFactory();
        }

        public NeighborhoodLayer()
            : this(new MooreNeighborhoodFactory())
        {

        }

        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (var vertex in graph)
            {
                var neighborhood = factory.CreateNeighborhood(vertex.Position);
                var neighbours = GetNeighboursWithinGraph(neighborhood, graph);
                vertex.Neighbors = neighbours;
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
            return self.Where(IsInRange).Distinct()
                .Select(graph.Get).ToList();
        }
    }
}
