using JsonFlatFileDataStore;
using Newtonsoft.Json;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    public sealed class JsonGraph : IIdentityItem<long>
    {
        public JsonGraph() { }

        [JsonConstructor]
        public JsonGraph(long id, GraphJsonCoordinates[] range, int[] dimensions, bool[] statuses, int[] costs, GraphJsonCoordinates[] coordinates, JsonNeighbours[] neighbours)
        {
            Id = id;
            Range = range;
            Dimensions = dimensions;
            Statuses = statuses;
            Costs = costs;
            Coordinates = coordinates;
            Neighbours = neighbours;
        }

        [JsonProperty]
        public long Id { get; set; }

        [JsonProperty]
        public GraphJsonCoordinates[] Range { get; set; }

        [JsonProperty]
        public int[] Dimensions { get; set; }

        [JsonProperty]
        public bool[] Statuses { get; set; }

        [JsonProperty]
        public int[] Costs { get; set; }

        [JsonProperty]
        public GraphJsonCoordinates[] Coordinates { get; set; }

        [JsonProperty]
        public JsonNeighbours[] Neighbours { get; set; }
    }

    public sealed class GraphJsonCoordinates
    {
        public GraphJsonCoordinates() { }

        public int[] Coordinates { get; set; }
    }

    public sealed class JsonRange
    {
        
        public JsonRange() { }

        public List<GraphJsonCoordinates> Range { get; set; }
    }

    public sealed class JsonNeighbours
    {
        public JsonNeighbours() { }

        public GraphJsonCoordinates[] Neighbors { get; set; }
    }

    internal sealed class GraphJsonRepository : JsonRepository<GraphModel, JsonGraph>
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

        protected override JsonGraph Map(GraphModel item)
        {
            return new JsonGraph
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

        protected override GraphModel Map(JsonGraph model)
        {
            var modelCoordinates = model.Coordinates;
            var vertices = modelCoordinates
                .Select(c => 
                {
                    return vertexFactory.CreateVertex(factory.CreateCoordinate(c.Coordinates));
                })
                .ToDictionary(item => item.Position);
            var modelNeighbours = model.Neighbours;
            var neighbours = modelCoordinates
                .Zip(modelNeighbours, (c, n) => (Pos: c, Neighb: n));
            foreach (var info in neighbours)
            {
                dynamic pos = info.Pos;
                var coordinate = factory.CreateCoordinate((int[]) pos.Coordinates);
                var vertex = vertices[coordinate];
                var neighborhood = info.Neighb;
                vertex.Neighbours = neighborhood.Neighbors
                    .Select(n => 
                    {
                        return factory.CreateCoordinate(n.Coordinates);
                    })
                    .Select(n => (IVertex)vertices[n])
                    .ToList();
            }
            var graph = graphFactory.CreateGraph(vertices.Values, model.Dimensions);
            var modelRange = model.Range;
            var range = modelRange.Select(r =>
            {
                return factory.CreateCoordinate(r.Coordinates);
            }).ToArray();
            return new GraphModel { Id = model.Id, Range = range, Graph = graph };
        }
    }
}
