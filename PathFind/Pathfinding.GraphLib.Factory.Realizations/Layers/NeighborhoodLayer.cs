using Pathfinding.GraphLib.Core.Interface;
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

        public NeighborhoodLayer(INeighborhoodFactory factory)
        {
            this.factory = factory;
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

        private static IReadOnlyCollection<IVertex> GetNeighboursWithinGraph(INeighborhood self, 
            IGraph<IVertex> graph)
        {
            bool IsWithin(int coordinate, int graphDimension)
            {
                var range = new InclusiveValueRange<int>(graphDimension - 1);
                return range.Contains(coordinate);
            }
            bool IsWithinGraph(ICoordinate neighbour)
            {
                return neighbour.Juxtapose(graph.DimensionsSizes, IsWithin);
            }
            return self.Where(IsWithinGraph).Select(graph.Get).OfType<IVertex>().ToArray();
        }
    }
}
