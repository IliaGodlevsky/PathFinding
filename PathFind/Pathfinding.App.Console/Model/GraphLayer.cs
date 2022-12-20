using Pathfinding.GraphLib.Core.Interface.Comparers;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
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
            var comparer = new CoordinateEqualityComparer();
            layer.Select(vertex => vertex.Position)
                .Intersect(graph.Select(vertex => vertex.Position), comparer)
                .ForEach(coordinate =>
                {
                    var vertex = graph.Get(coordinate);
                    var layerVertex = layer.Get(coordinate);
                    int cost = layerVertex.Cost.CurrentCost;
                    vertex.Cost = vertex.Cost.SetCost(cost);
                    vertex.IsObstacle = layerVertex.IsObstacle;
                });
        }
    }
}