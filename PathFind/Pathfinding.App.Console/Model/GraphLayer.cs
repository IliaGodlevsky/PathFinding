using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
using Pathfinding.GraphLib.Core.Interface.Extensions;
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
            layer.GetCoordinates()
                .Intersect(graph.GetCoordinates(), comparer)
                .ForEach(coordinate => Overlay(coordinate, graph));
        }

        private void Overlay(ICoordinate coordinate, Graph2D<Vertex> graph)
        {
            var vertex = graph.Get(coordinate);
            var layerVertex = layer.Get(coordinate);
            int cost = layerVertex.Cost.CurrentCost;
            vertex.Cost = vertex.Cost.SetCost(cost);
            vertex.IsObstacle = layerVertex.IsObstacle;
        }
    }
}