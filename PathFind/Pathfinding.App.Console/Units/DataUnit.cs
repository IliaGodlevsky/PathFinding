using GalaSoft.MvvmLight.Messaging;
using LiteDB;
using Newtonsoft.Json;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.ReadDto;
using Pathfinding.App.Console.DataAccess.Repo;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.Units
{
    internal sealed class DataUnit : Unit, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IDbContext context;
        private readonly IGraphFactory<Vertex> graphFactory;
        private readonly IVertexFactory<Vertex> vertexFactory;
        private readonly IVertexCostFactory costFactory;
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer;

        private readonly GraphsPathfindingHistory cache = new();

        public DataUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IMessenger messenger,
            IDbContext context,
            IGraphFactory<Vertex> graphFactory,
            IVertexFactory<Vertex> vertexFactory,
            IVertexCostFactory costFactory,
            ISerializer<IEnumerable<ICoordinate>> serializer)
            : base(menuItems)
        {
            this.messenger = messenger;
            this.context = context;
            this.graphFactory = graphFactory;
            this.vertexFactory = vertexFactory;
            this.costFactory = costFactory;
            this.serializer = serializer;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<NeighbourAddedMessage>(this, Tokens.Data, NeighbourAdded);
            messenger.Register<NeighbourDeletedMessage>(this, Tokens.Data, NeighborDeleted);
            messenger.Register<VertexChangedMessage>(this, Tokens.Data, OnVertexChanged);
            messenger.Register<GetAllGraphsMessage>(this, Tokens.Data, GetAllGraphs);
            messenger.Register<GetGraphMessage>(this, Tokens.Data, GetGraph);
            messenger.RegisterGraph(this, Tokens.Data, OnGraphAdded);
            messenger.Register<AlgorithmStatisticsMessage>(this, Tokens.Data, OnAlgorithmFinished);
        }

        private void OnAlgorithmFinished(AlgorithmStatisticsMessage msg)
        {
            var algorithm = new AlgorithmEntity
            {
                GraphId = msg.GraphId,
                Statistics = JsonConvert.SerializeObject(msg.Statistics),
                Costs = JsonConvert.SerializeObject(msg.Costs),
                Range = CreateString(msg.Range),
                Path = CreateString(msg.Path),
                Obstacles = CreateString(msg.Obstacles),
                VisitedVertices = CreateString(msg.Visited)
            };
            context.Algorithms.Create(algorithm);
        }

        private void NeighbourAdded(NeighbourAddedMessage msg)
        {
            int vertexId = msg.Vertex.Id;
            int neighbourId = msg.VertexNeighbour.Id;
            var neighbourEntity = new NeighboursEntity()
            {
                VertexId = vertexId,
                NeighbourId = neighbourId,
            };
            context.Neighbours.Create(neighbourEntity);
        }

        private void OnVertexChanged(VertexChangedMessage msg)
        {
            var vertex = msg.Vertex;
            var entity = new VertexEntity
            {
                Id = vertex.Id,
                X = vertex.Position.GetX(),
                Y = vertex.Position.GetY(),
                IsObstacle = vertex.IsObstacle,
                Cost = vertex.Cost.CurrentCost
            };
            context.Vertices.Update(entity);
        }

        private void NeighborDeleted(NeighbourDeletedMessage msg)
        {
            int vertexId = msg.Vertex.Id;
            int neighbourId = msg.Neighbour.Id;
            var found = context.Neighbours
                .GetAll()
                .FirstOrDefault(e => e.VertexId == vertexId && e.NeighbourId == neighbourId);
            if (found != null)
            {
                context.Neighbours.Delete(found.Id);
            }
        }

        private void GetAllGraphs(GetAllGraphsMessage msg)
        {
            var graphs = context.Graphs.GetAll()
                .Select(g => new GraphReadDto
                {
                    Id = g.Id,
                    Parametres = new[] { g.Width, g.Length }
                }).ToArray();
            msg.Graphs = graphs;
        }

        private void GetGraph(GetGraphMessage msg)
        {
            int id = msg.Id;
            var graphEntity = context.Graphs.Read(id);
            var map = context.GetVerticesWithNeighbours(id);
            var costs = context.GetCostRangesForVertices(map.Keys);
            var vertices = (vertexFactory, costFactory).CreateVertices(map.Keys, costs);
            var parametres = new[] { graphEntity.Width, graphEntity.Length };
            var graph = graphFactory.CreateGraph(vertices, parametres);
            foreach (var item in map)
            {
                var coordinate = new Coordinate(item.Key.X, item.Key.Y);
                var neighbours = item.Value
                    .Select(v => new Coordinate(v.X, v.Y))
                    .Select(graph.Get)
                    .ToHashSet();
                var vertex = graph.Get(coordinate);
                vertex.Neighbours.AddRange(neighbours);
            }
            msg.Respond = graph;
        }

        private void OnGraphAdded(GraphMessage msg)
        {
            var entity = context.AddGraph(msg.Graph);
            var vertexEntities = context.AddVertices(msg.Graph, entity.Id);
            context.AddRanges(msg.Graph, vertexEntities);
            context.AddNeighbours(msg.Graph, vertexEntities);
            msg.Graph.Zip(vertexEntities, (v, e) => (Vertex: v, Entity: e))
                .ForEach(i => i.Vertex.Id = i.Entity.Id);
        }

        private void GetAlgorithmsData(GetAlgorithmsMessage msg)
        {
            var algorithms = context.Algorithms
                .GetAll()
                .Where(e => e.GraphId == msg.GraphId)
                .ToList();
            var readDtos = new List<AlgorithmReadDto>();
            foreach (var algorithm in algorithms)
            {
                var dto = new AlgorithmReadDto()
                {
                    Id = algorithm.Id,
                    Statistics = JsonConvert.DeserializeObject<Statistics>(algorithm.Statistics),
                    Costs = JsonConvert.DeserializeObject<int[]>(algorithm.Costs),
                    Visited = CreateCoordinates(algorithm.VisitedVertices),
                    Range = CreateCoordinates(algorithm.Range),
                    Obstacles = CreateCoordinates(algorithm.Obstacles),
                    Path = CreateCoordinates(algorithm.Path),
                };
                readDtos.Add(dto);
            }
            msg.Algorithms = readDtos;
        }

        private IReadOnlyCollection<ICoordinate> CreateCoordinates(string value)
        {
            var asBytes = Encoding.UTF8.GetBytes(value);
            using (var stream = new MemoryStream(asBytes))
            {
                var coordinates = serializer.DeserializeFrom(stream);
                return coordinates.ToArray();
            }
        }

        private string CreateString(IEnumerable<ICoordinate> coordinates)
        {
            using (var stream = new MemoryStream())
            {
                serializer.SerializeTo(coordinates, stream);
                var bytes = stream.ToArray();
                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
}
