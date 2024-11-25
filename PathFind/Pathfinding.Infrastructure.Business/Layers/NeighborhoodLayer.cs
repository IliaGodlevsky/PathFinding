using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public abstract class NeighborhoodLayer : ILayer
    {
        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (var vertex in graph)
            {
                var neighborhood = CreateNeighborhood(vertex.Position);
                var neighbours = GetNeighboursWithinGraph(neighborhood, graph);
                vertex.Neighbors = neighbours;
            }
        }

        protected abstract INeighborhood CreateNeighborhood(Coordinate coordinate);

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
