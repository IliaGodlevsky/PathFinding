using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.Repo;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IDbContextExtensions
    {
        public static IReadOnlyDictionary<VertexEntity, List<VertexEntity>> GetVerticesWithNeighbours(this IDbContext context, int graphId)
        {
            var vertices = context.Vertices
                .GetAll()
                .Where(v => v.GraphId == graphId)
                .ToDictionary(item => item.Id, item => item);
            var neighbours = context.Neighbours.GetAll();
            return vertices.Values.GroupJoin(neighbours, 
                vertex => vertex.Id, neighbour => neighbour.VertexId,
                (vertex, vertexNeighbours) => new
                {
                    Vertex = vertices[vertex.Id],
                    Neighbours = vertexNeighbours
                        .Select(vn => vertices[vn.NeighbourId])
                        .ToList()
                })
                .ToDictionary(item => item.Vertex, item => item.Neighbours);
        }

        public static IReadOnlyDictionary<int, InclusiveValueRange<int>> GetCostRangesForVertices(this IDbContext context, IEnumerable<VertexEntity> vertices)
        {
            return context.VerticesCosts.GetAll().Join(vertices,
                c => c.VertexId, v => v.Id, (c, v)
                => new
                {
                    Id = v.Id,
                    Range = new InclusiveValueRange<int>(c.UpperValueOfRange, c.LowerValueOfRange)
                }).ToDictionary(item => item.Id, item => item.Range);
        }

        public static IReadOnlyCollection<Vertex> CreateVertices(this (IVertexFactory<Vertex> VertexFactory, IVertexCostFactory CostFactory) factories,
            IEnumerable<VertexEntity> entities, IReadOnlyDictionary<int, InclusiveValueRange<int>> ranges)
        {
            var vertices = new List<Vertex>();
            foreach (var item in entities)
            {
                var coordinate = new Coordinate(item.X, item.Y);
                var vertex = factories.VertexFactory.CreateVertex(coordinate);
                vertex.IsObstacle = item.IsObstacle;
                var costRange = ranges[item.Id];
                vertex.Cost = factories.CostFactory.CreateCost(item.Cost, costRange);
                vertices.Add(vertex);
            }
            return vertices;
        }

        public static void AddRanges(this IDbContext context, 
            IEnumerable<Vertex> vertices, IEnumerable<VertexEntity> entities)
        {
            var ranges = vertices.Zip(entities, (v, c) => new CostRangeEntity
            {
                VertexId = c.Id,
                LowerValueOfRange = v.Cost.CostRange.LowerValueOfRange,
                UpperValueOfRange = v.Cost.CostRange.UpperValueOfRange,
            }).ToArray();
            ranges = context.VerticesCosts.Create(ranges);
        }

        public static void AddNeighbours(this IDbContext context, IEnumerable<Vertex> vertices, IEnumerable<VertexEntity> entities)
        {
            var map = entities
                .ToDictionary(item => (ICoordinate)new Coordinate(item.X, item.Y), item => item);
            var neighbours = vertices.Zip(entities, (v, e) => new
            {
                Id = e.Id,
                Neighbours = v.Neighbours.Select(n => map[n.Position].Id)
            }).SelectMany(item => item.Neighbours, (item, id) => new NeighboursEntity
            {
                VertexId = item.Id,
                NeighbourId = id
            }).ToList();
            context.Neighbours.Create(neighbours);
        }

        public static IEnumerable<VertexEntity> AddVertices(this IDbContext context, IEnumerable<Vertex> vertices, int graphId)
        {
            var vertexEntities = vertices.Select(v => new VertexEntity
            {
                IsObstacle = v.IsObstacle,
                X = v.Position.GetX(),
                Y = v.Position.GetY(),
                GraphId = graphId,
                Cost = v.Cost.CurrentCost
            });
            return context.Vertices.Create(vertexEntities);
        }

        public static GraphEntity AddGraph(this IDbContext context, IGraph<Vertex> graph)
        {
            var graphEntity = new GraphEntity
            {
                Width = graph.GetWidth(),
                Length = graph.GetLength()
            };
            return context.Graphs.Create(graphEntity);
        }
    }
}
