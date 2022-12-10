using Pathfinding.GraphLib.Core.Abstractions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using System.Linq;

namespace Pathfinding.App.Console.Model
{
    internal sealed class GraphLayer : ILayer<Graph2D<Vertex>, Vertex>
    {
        private readonly Graph2D<Vertex> layer;

        public GraphLayer(Graph2D<Vertex> graph)
        {
            this.layer = graph;
        }

        public void Overlay(Graph2D<Vertex> graph)
        {
            var coordinates = layer.Select(vertex => vertex.Position)
                .Intersect(graph.Select(vertex => vertex.Position))
                .ToArray();
            foreach(var coordinate in coordinates)
            {
                var vertex = graph.Get(coordinate);
                var layerVertex = layer.Get(coordinate);
                vertex.Cost = vertex.Cost.SetCost(layerVertex.Cost.CurrentCost);
                vertex.IsObstacle = layerVertex.IsObstacle;
            }
        }
    }
}
