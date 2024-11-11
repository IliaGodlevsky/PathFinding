using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class GraphLayer : ILayer
    {
        private readonly IReadOnlyCollection<IVertex> vertices;

        public GraphLayer(IGraph<IVertex> graph)
        {
            this.vertices = graph;
        }

        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (var vertex in vertices)
            {
                var vert = graph.Get(vertex.Position);
                vert.IsObstacle = vertex.IsObstacle;
                vert.Cost = vertex.Cost.DeepClone();
                vert.Neighbors = vertex.Neighbors
                    .GetCoordinates()
                    .Select(graph.Get)
                    .ToArray();
            }
        }
    }
}
