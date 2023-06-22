using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Entities.JsonEntities;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos
{

    internal sealed class GraphJsonRepository : JsonRepository<GraphModel, JsonGraphEntity>
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

        protected override JsonGraphEntity Map(GraphModel item)
        {
            return new JsonGraphEntity
            {
                Id = item.Id,
                Dimensions = item.Graph.DimensionsSizes.ToArray(),
                Costs = item.Graph.Select(v => v.Cost.CurrentCost).ToArray(),
                Statuses = item.Graph.Select(v => v.IsObstacle).ToArray(),
                Neighbours = item.Graph.Select(v =>
                {
                    var neighbours = v.Neighbours
                        .Select(n => new GraphJsonCoordinates { Coordinates = n.Position.ToArray() })
                        .ToArray();
                    return new JsonNeighbours { Neighbors = neighbours };
                }).ToArray(),
                Coordinates = item.Graph
                    .Select(v => new GraphJsonCoordinates { Coordinates = v.Position.ToArray() })
                    .ToArray(),
                Range = item.Range
                    .Select(c => new GraphJsonCoordinates { Coordinates = c.ToArray() })
                    .ToArray()
            };
        }

        protected override GraphModel Map(JsonGraphEntity model)
        {
            var modelCoordinates = model.Coordinates;
            var vertices = modelCoordinates
                .Select(c => vertexFactory.CreateVertex(factory.CreateCoordinate(c.Coordinates)))
                .ToDictionary(item => item.Position);
            var neighbours = modelCoordinates.Zip(model.Neighbours, (c, n) => (Pos: c, Neighb: n));
            foreach (var info in neighbours)
            {
                var coordinate = factory.CreateCoordinate(info.Pos.Coordinates);
                var vertex = vertices[coordinate];
                var neighborhood = info.Neighb;
                vertex.Neighbours = neighborhood.Neighbors
                    .Select(n =>factory.CreateCoordinate(n.Coordinates))
                    .Select(n => (IVertex)vertices[n])
                    .ToList();
            }
            var graph = graphFactory.CreateGraph(vertices.Values, model.Dimensions);
            var modelRange = model.Range;
            var range = modelRange.Select(r => factory.CreateCoordinate(r.Coordinates)).ToArray();
            return new GraphModel { Id = model.Id, Range = range, Graph = graph };
        }
    }
}
