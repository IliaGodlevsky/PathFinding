using LiteDB;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.ReadDto;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using Shared.Extensions;
using System.Linq;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Newtonsoft.Json;
using Pathfinding.App.Console.Model.Notes;

namespace Pathfinding.App.Console.DataAccess.Repo
{
    internal sealed class LiteDbContextService : IDbContextService
    {
        private const string Algorithms = nameof(Algorithms);
        private const string Graphs = nameof(Graphs);
        private const string Vertices = nameof(Vertices);
        private const string Neighbours = nameof(Neighbours);
        private const string PathfindingRanges = nameof(PathfindingRanges);
        private const string VerticesCostRanges = nameof(VerticesCostRanges);

        private const string ConnectionString = Constants.LiteDbConnectionString;

        private readonly GraphsPathfindingHistory memory = new();

        private readonly IGraphFactory<Vertex> graphFactory;
        private readonly IVertexFactory<Vertex> vertexFactory;
        private readonly IVertexCostFactory costFactory;
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer;

        public LiteDbContextService(IGraphFactory<Vertex> graphFactory,
            IVertexFactory<Vertex> vertexFactory,
            IVertexCostFactory costFactory,
            ISerializer<IEnumerable<ICoordinate>> serializer)
        {
            this.graphFactory = graphFactory;
            this.vertexFactory = vertexFactory;
            this.costFactory = costFactory;
            this.serializer = serializer;
        }

        private ILiteCollection<T> GetCollection<T>(LiteDatabase db, string name)
        {
            return db.GetCollection<T>(name, BsonAutoId.Int32);
        }

        public int AddGraph(IGraph<Vertex> graph)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var graphsRepo = GetCollection<GraphEntity>(db, Graphs);
                var verticesRepo = GetCollection<VertexEntity>(db, Vertices);
                var costRangeRepo = GetCollection<CostRangeEntity>(db, VerticesCostRanges);
                var neighboursRepo = GetCollection<NeighboursEntity>(db, Neighbours);

                var entity = new GraphEntity
                {
                    Obstacles = graph.GetObstaclesCount(),
                    Width = graph.GetWidth(),
                    Length = graph.GetHeight(),
                };
                graphsRepo.Insert(entity);

                var vertexEntities = graph.Select(v => new VertexEntity
                {
                    IsObstacle = v.IsObstacle,
                    X = v.Position.GetX(),
                    Y = v.Position.GetY(),
                    GraphId = entity.Id,
                    Cost = v.Cost.CurrentCost
                }).ToArray();
                verticesRepo.Insert(vertexEntities);

                var ranges = graph.Zip(vertexEntities, (v, c) => new CostRangeEntity
                {
                    VertexId = c.Id,
                    GraphId = entity.Id,
                    LowerValueOfRange = v.Cost.CostRange.LowerValueOfRange,
                    UpperValueOfRange = v.Cost.CostRange.UpperValueOfRange,
                }).ToArray();
                costRangeRepo.Insert(ranges);

                var map = vertexEntities
                    .ToDictionary(item => (ICoordinate)new Coordinate(item.X, item.Y), item => item);
                var neighbours = graph.Zip(vertexEntities, (v, e) => new
                {
                    Id = e.Id,
                    Neighbours = v.Neighbours.Select(n => map[n.Position].Id)
                }).SelectMany(item => item.Neighbours, (item, id) => new NeighboursEntity
                {
                    VertexId = item.Id,
                    NeighbourId = id,
                    GraphId = entity.Id,
                }).ToArray();
                neighboursRepo.Insert(neighbours);
                memory.Add(entity.Id, graph);
                return entity.Id;
            }
        }

        public void AddNeighbour(Vertex vertex, Vertex neighbour)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var neighbourRepo = GetCollection<NeighboursEntity>(db, Neighbours);
                var neighbourEntity = new NeighboursEntity()
                {
                    VertexId = vertex.Id,
                    NeighbourId = neighbour.Id,
                    GraphId = vertex.GraphId
                };
                neighbourRepo.Insert(neighbourEntity);
            }
        }

        public void AddRangeVertex(Vertex vertex, int order)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var rangeRepo = GetCollection<PathfindingRangeVertexEntity>(db, PathfindingRanges);
                var entity = new PathfindingRangeVertexEntity
                {
                    VertexId = vertex.Id,
                    Order = order,
                    GraphId = vertex.GraphId
                };
                var range = memory.GetRange(vertex.GraphId);
                range.Add((order, vertex.Position));
                rangeRepo.Insert(entity);
            }
        }

        public void DeleteNeighbour(Vertex vertex, Vertex neighbour)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var repo = GetCollection<NeighboursEntity>(db, Neighbours);
                var entity = repo.Query()
                    .Where(x => x.VertexId == vertex.Id && x.NeighbourId == neighbour.Id)
                    .ToEnumerable()
                    .FirstOrDefault();
                if (entity is not null)
                {
                    repo.Delete(entity.Id);
                }
            }
        }

        public IReadOnlyCollection<AlgorithmReadDto> GetAllAlgorithmsInfo(int graphId)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var repo = GetCollection<AlgorithmEntity>(db, Algorithms);
                var entities = repo.Find(x => x.GraphId == graphId);
                var result = new List<AlgorithmReadDto>();
                foreach (var entity in entities)
                {
                    int id = entity.Id;
                    if (memory.TryGetHistory(graphId, out var history))
                    {
                        if (history.Algorithms.Contains(id))
                        {
                            var dto = new AlgorithmReadDto
                            {
                                Id = id,
                                Statistics = history.Statistics[id],
                                Costs = history.Costs[id],
                                Range = history.Ranges[id],
                                Obstacles = history.Obstacles[id],
                                Visited = history.Visited[id],
                                Path = history.Paths[id],
                            };
                            result.Add(dto);
                        }
                    }
                    else
                    {
                        var dto = new AlgorithmReadDto
                        {
                            Id = id,
                            Costs = JsonConvert.DeserializeObject<int[]>(entity.Costs),
                            Statistics = JsonConvert.DeserializeObject<Statistics>(entity.Statistics),
                            Range = serializer.DeserializeFromString(entity.Range).ToArray(),
                            Obstacles = serializer.DeserializeFromString(entity.Obstacles).ToArray(),
                            Visited = serializer.DeserializeFromString(entity.VisitedVertices).ToArray(),
                            Path = serializer.DeserializeFromString(entity.Path).ToArray(),
                        };
                        history = new GraphPathfindingHistory();
                        history.Algorithms.Add(entity.Id);
                        history.Paths.TryGetOrAddNew(id).AddRange(dto.Path);
                        history.Obstacles.TryGetOrAddNew(id).AddRange(dto.Obstacles);
                        history.Visited.TryGetOrAddNew(id).AddRange(dto.Visited);
                        history.Ranges.TryGetOrAddNew(id).AddRange(dto.Range);
                        history.Statistics.Add(id, dto.Statistics);
                        history.Costs.TryGetOrAddNew(id).AddRange(dto.Costs);
                        memory.Add(graphId, history);
                    }
                }
                return result;
            }
        }

        public void AddAlgorithm(AlgorithmCreateDto algorithm)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var repo = GetCollection<AlgorithmEntity>(db, Algorithms);
                var entity = new AlgorithmEntity
                {
                    GraphId = algorithm.GraphId,
                    Costs = JsonConvert.SerializeObject(algorithm.Costs.ToArray()),
                    Statistics = JsonConvert.SerializeObject(algorithm.Statistics),
                    Range = serializer.SerializeToString(algorithm.Range),
                    Obstacles = serializer.SerializeToString(algorithm.Obstacles),
                    VisitedVertices = serializer.SerializeToString(algorithm.Visited),
                    Path = serializer.SerializeToString(algorithm.Path)
                };
                repo.Insert(entity);
                int id = entity.Id;
                var history = new GraphPathfindingHistory();
                history.Algorithms.Add(entity.Id);
                history.Paths.TryGetOrAddNew(id).AddRange(algorithm.Path);
                history.Obstacles.TryGetOrAddNew(id).AddRange(algorithm.Obstacles);
                history.Visited.TryGetOrAddNew(id).AddRange(algorithm.Visited);
                history.Ranges.TryGetOrAddNew(id).AddRange(algorithm.Range);
                history.Statistics.Add(id, algorithm.Statistics);
                history.Costs.TryGetOrAddNew(id).AddRange(algorithm.Costs);
                memory.Add(entity.GraphId, history);
            }
        }

        public IReadOnlyCollection<IGraph<Vertex>> GetAllGraphs()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var graphRepo = GetCollection<GraphEntity>(db, Graphs);
                var verticesRepo = GetCollection<VertexEntity>(db, Vertices);
                var neighbourRepo = GetCollection<NeighboursEntity>(db, Neighbours);
                var costRangesRepo = GetCollection<CostRangeEntity>(db, VerticesCostRanges);

                var ids = graphRepo.FindAll().Select(x => x.Id).ToArray();
                var graphs = new List<IGraph<Vertex>>();

                foreach (var id in ids)
                {
                    if (memory.TryGetGraph(id, out var graph))
                    {
                        graphs.Add(graph);
                    }
                    else
                    {
                        graph = AssembleGraph(graphRepo, verticesRepo,
                            neighbourRepo, costRangesRepo, id);
                        graphs.Add(graph);
                        memory.Add(id, graph);
                    }
                }
                return graphs;
            }
        }

        public IReadOnlyCollection<GraphReadDto> GetAllGraphsInfo()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var repo = GetCollection<GraphEntity>(db, Graphs);
                return repo.FindAll().Select(x => new GraphReadDto
                {
                    Id = x.Id,
                    Length = x.Length,
                    Width = x.Width,
                    ObstaclesCount = x.Obstacles,
                }).ToArray();
            }
        }

        public IGraph<Vertex> GetGraph(int graphId)
        {
            if (memory.TryGetGraph(graphId, out var graph))
            {
                return graph;
            }
            using (var db = new LiteDatabase(ConnectionString))
            {
                var graphRepo = GetCollection<GraphEntity>(db, Graphs);
                var verticesRepo = GetCollection<VertexEntity>(db, Vertices);
                var neighbourRepo = GetCollection<NeighboursEntity>(db, Neighbours);
                var costRangesRepo = GetCollection<CostRangeEntity>(db, VerticesCostRanges);
                graph = AssembleGraph(graphRepo, verticesRepo,
                    neighbourRepo, costRangesRepo, graphId);
                memory.Add(graphId, graph);
                return graph;
            }
        }

        public void RemoveRangeVertex(Vertex vertex)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var rangeRepo = GetCollection<PathfindingRangeVertexEntity>(db, PathfindingRanges);
                var entity = rangeRepo.FindOne(x => x.VertexId == vertex.Id);
                rangeRepo.Delete(entity.Id);
                var range = memory.GetRange(vertex.GraphId);
                range.RemoveWhere(x => x.Item2.Equals(vertex));
            }
        }

        public void UpdateVertexCost(Vertex vertex)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var verticesRepo = GetCollection<VertexEntity>(db, Vertices);
                var entity = verticesRepo.FindById(vertex.Id);
                entity.Cost = vertex.Cost.CurrentCost;
                verticesRepo.Update(entity);
            }
        }

        public IReadOnlyCollection<ICoordinate> GetPathfindingRange(int graphId)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var repo = GetCollection<PathfindingRangeVertexEntity>(db, PathfindingRanges);
                var vertexRepo = GetCollection<VertexEntity>(db, Vertices);
                var vertices = vertexRepo.Find(x => x.GraphId == graphId);
                var tuples = repo.Find(x => x.GraphId == graphId)
                    .Join(vertices,
                    x => x.VertexId, x => x.Id,
                    (x, y) => (Order: x.Order, Value: new Coordinate(y.X, y.Y)))
                    .ToArray();
                var ranges = tuples
                    .OrderBy(x => x.Order)
                    .Select(x => x.Value)
                    .ToArray();
                var range = memory.GetRange(graphId);
                tuples.ForEach(x => range.Add(x));
                return ranges;
            }
        }

        public void UpdateVertexObstacleState(Vertex vertex)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var graphsRepo = GetCollection<GraphEntity>(db, Graphs);
                var verticesRepo = GetCollection<VertexEntity>(db, Vertices);
                var vertexEntity = verticesRepo.FindById(vertex.Id);
                var graphEntity = graphsRepo.FindById(vertex.GraphId);
                vertexEntity.IsObstacle = vertex.IsObstacle;
                int increment = vertex.IsObstacle ? 1 : -1;
                graphEntity.Obstacles += increment;
                graphsRepo.Update(graphEntity);
                verticesRepo.Update(vertexEntity);
            }
        }

        private static IReadOnlyDictionary<int, VertexEntity> GetVertices(ILiteCollection<VertexEntity> repo, int graphId)
        {
            return repo.Query()
                    .Where(v => v.GraphId == graphId)
                    .ToEnumerable()
                    .ToDictionary(item => item.Id, item => item);
        }

        private static Dictionary<VertexEntity, List<VertexEntity>> GetNeighboursMap(IReadOnlyDictionary<int, VertexEntity> vertices, 
            ILiteCollection<NeighboursEntity> repo, int graphId)
        {
            var neighboursEntities = repo.Find(x => x.GraphId == graphId);
            return vertices.Values.GroupJoin(neighboursEntities,
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

        private static IReadOnlyDictionary<int, InclusiveValueRange<int>> GetRangesMap(ILiteCollection<CostRangeEntity> repo, 
            IEnumerable<VertexEntity> vertexEntities, int graphId)
        {
            var costsRanges = repo.Find(x => x.GraphId == graphId);
            return costsRanges.Join(vertexEntities,
                    c => c.VertexId, v => v.Id, (c, v)
                    => new
                    {
                        Id = v.Id,
                        Range = new InclusiveValueRange<int>(c.UpperValueOfRange, c.LowerValueOfRange)
                    }).ToDictionary(item => item.Id, item => item.Range);
        }

        private IReadOnlyCollection<Vertex> CreateVertices(IEnumerable<VertexEntity> vertices,
            IReadOnlyDictionary<int, InclusiveValueRange<int>> rangeMap)
        {
            var realVertices = new List<Vertex>();
            foreach (var item in vertices)
            {
                var coordinate = new Coordinate(item.X, item.Y);
                var vertex = vertexFactory.CreateVertex(coordinate);
                vertex.IsObstacle = item.IsObstacle;
                var costRange = rangeMap[item.Id];
                vertex.Cost = costFactory.CreateCost(item.Cost, costRange);
                realVertices.Add(vertex);
            }
            return realVertices;
        }

        private void FillNeighbours(IGraph<Vertex> graph, 
            IReadOnlyDictionary<VertexEntity, List<VertexEntity>> neighbourMap)
        {
            foreach (var item in neighbourMap)
            {
                var coordinate = new Coordinate(item.Key.X, item.Key.Y);
                var neighbours = item.Value
                    .Select(v => new Coordinate(v.X, v.Y))
                    .Select(graph.Get)
                    .ToHashSet();
                var vertex = graph.Get(coordinate);
                vertex.Neighbours.AddRange(neighbours);
            }
        }

        private IGraph<Vertex> AssembleGraph(ILiteCollection<GraphEntity> graphRepo,
            ILiteCollection<VertexEntity> verticesRepo,
            ILiteCollection<NeighboursEntity> neighbourRepo,
            ILiteCollection<CostRangeEntity> costRangesRepo,
            int graphId)
        {
            var graphEntity = graphRepo.FindOne(x => x.Id == graphId);
            var vertices = GetVertices(verticesRepo, graphId);
            var neighboursEntities = neighbourRepo.Find(x => x.GraphId == graphId);
            var map = GetNeighboursMap(vertices, neighbourRepo, graphId);
            var rangesMap = GetRangesMap(costRangesRepo, vertices.Values, graphId);
            var realVertices = CreateVertices(vertices.Values, rangesMap);
            var parametres = new[] { graphEntity.Width, graphEntity.Length };
            var graph = graphFactory.CreateGraph(realVertices, parametres);
            FillNeighbours(graph, map);
            FillVerticesIds(graph, map.Keys);
            return graph;
        }

        private void FillVerticesIds(IGraph<Vertex> graph, IEnumerable<VertexEntity> vertices)
        {
            foreach (var vertex in vertices)
            {
                var coordinate = new Coordinate(vertex.X, vertex.Y);
                var graphVertex = graph.Get(coordinate);
                graphVertex.Id = vertex.Id;
                graphVertex.GraphId = vertex.GraphId;
            }
        }
    }
}
