using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class GraphJsonRepository : JsonRepository<GraphModel>
    {
        private readonly ICoordinateFactory factory;
        private readonly IVertexFactory<Vertex> vertexFactory;
        private readonly IGraphFactory<Graph2D<Vertex>, Vertex> graphFactory;

        protected override string Table { get; } = "graphs";

        public GraphJsonRepository(DataStore storage, 
            ICoordinateFactory factory,
            IVertexFactory<Vertex> vertexFactory, 
            IGraphFactory<Graph2D<Vertex>, Vertex> graphFactory) 
            : base(storage)
        {
            this.factory = factory;
            this.vertexFactory = vertexFactory;
            this.graphFactory = graphFactory;
        }

        protected override dynamic Map(GraphModel item)
        {
            dynamic result = new ExpandoObject();
            result.Id = item.Id;
            result.Dimensions = item.Graph.DimensionsSizes.ToArray();
            result.Costs = item.Graph.Select(v => v.Cost.CurrentCost).ToArray();
            result.Statuses = item.Graph.Select(v => v.IsObstacle).ToArray();
            result.Neighbours = item.Graph.Select(v =>
            {
                var neighbours = v.Neighbours
                    .Select(n => new { Coordinates = n.Position.ToArray() })
                    .ToArray();
                return new { Neighbours = neighbours };
            }).ToArray();
            result.Coordinates = item.Graph
                .Select(v => new { Coordinates = v.Position.ToArray() })
                .ToArray();
            result.Range = item.Range
                .GetCoordinates()
                .Select(c => new { Coordinates = c.ToArray() })
                .ToArray();
            return result;
        }

        protected override GraphModel Map(dynamic model)
        {
            var modelCoordinates = (IEnumerable<object>)model.Coordinates;
            var vertices = modelCoordinates
                .Select(c => 
                {
                    dynamic r = c;
                    return vertexFactory.CreateVertex(factory.CreateCoordinate((int[])r.Coordinates));
                })
                .ToDictionary(item => item.Position);
            var modelNeighbours = (IEnumerable<object>)model.Neighbours;
            var neighbours = modelCoordinates
                .Zip(modelNeighbours, (c, n) => (Pos: c, Neighb: n));
            foreach (var info in neighbours)
            {
                dynamic pos = info.Pos;
                var coordinate = factory.CreateCoordinate((int[]) pos.Coordinates);
                var vertex = vertices[coordinate];
                var neighborhood = (IEnumerable<object>)info.Neighb;
                vertex.Neighbours = neighborhood
                    .Select(n => 
                    {
                        dynamic obj = n;
                        return factory.CreateCoordinate((int[])obj.Coordinates);
                    })
                    .Select(n => (IVertex)vertices[n])
                    .ToList();
            }
            var graph = (Graph2D<Vertex>)graphFactory.CreateGraph(vertices.Values, model.Dimensions);
            var modelRange = (IEnumerable<object>)model.Range;
            var range = modelRange.Select(r =>
            {
                dynamic o = r;
                return factory.CreateCoordinate((int[])o.Coordinates);
            })
                .Select(graph.Get)
                .ToArray();
            return new GraphModel { Id = model.Id, Range = range, Graph = graph };
        }
    }
}
