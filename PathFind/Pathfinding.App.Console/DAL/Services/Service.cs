using AutoMapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.DAL.UOF.Factories;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Services
{
    internal sealed class Service : IService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWorkFactory factory;

        public Service(IMapper mapper, IUnitOfWorkFactory factory = null)
        {
            this.mapper = mapper;
            this.factory = factory ?? new InMemoryUnitOfWorkFactory();
        }

        public int AddAlgorithm(AlgorithmCreateDto algorithm)
        {
            return Transaction(unitOfWork =>
            {
                var entity = mapper.Map<AlgorithmEntity>(algorithm);
                unitOfWork.AlgorithmsRepository.AddOne(entity);
                return entity.Id;
            });
        }

        public int AddGraph(IGraph<Vertex> dto)
        {
            return Transaction(unitOfWork => unitOfWork.AddGraph(mapper, dto));
        }

        public IReadOnlyCollection<PathfindingHistoryReadDto> AddPathfindingHistory(IEnumerable<PathfindingHistoryCreateDto> histories)
        {
            return Transaction(unitOfWork => histories.Select(history =>
            {
                int id = unitOfWork.AddGraph(mapper, history.Graph);
                var algorithms = mapper.Map<AlgorithmEntity[]>(history.Algorithms);
                algorithms.ForEach(a => a.GraphId = id);
                unitOfWork.AlgorithmsRepository.AddMany(algorithms);
                var vertices = history.Range
                    .Select((x, i) => (Order: i, Vertex: history.Graph.Get(x)));
                var entities = SelectRangeEntities(vertices, id);
                unitOfWork.RangeRepository.AddRange(entities);
                var read = mapper.Map<PathfindingHistoryReadDto>(history);
                read.Id = id;
                return read;
            }).ToReadOnly());
        }

        public bool AddRange(IEnumerable<(int Order, Vertex Vertex)> vertices, int graphId)
        {
            return Transaction(unitOfWork =>
            {
                var list = SelectRangeEntities(vertices, graphId);
                unitOfWork.RangeRepository.AddRange(list);
                return true;
            });
        }

        public bool DeleteGraph(int graphId)
        {
            return Transaction(unitOfWork => unitOfWork.GraphRepository.DeleteGraph(graphId));
        }

        public IReadOnlyList<GraphEntity> GetAllGraphInfo()
        {
            return Transaction(unitOfWork => unitOfWork.GraphRepository.GetAll().ToList().AsReadOnly());
        }

        public IGraph<Vertex> GetGraph(int id)
        {
            return Transaction(unitOfWork => unitOfWork.CreateGraph(id, mapper));
        }

        public IReadOnlyCollection<int> GetGraphIds()
        {
            return Transaction(unitOfWork =>
            {
                return unitOfWork.GraphRepository.GetAll().Select(x => x.Id).ToReadOnly();
            });
        }

        public IReadOnlyCollection<AlgorithmReadDto> GetGraphPathfindingHistory(int graphId)
        {
            return Transaction(unitOfWork => unitOfWork.GetAlgorithms(graphId, mapper));
        }

        public IReadOnlyCollection<ICoordinate> GetRange(int graphId)
        {
            return Transaction(unitOfWork => unitOfWork.GetRange(graphId));
        }

        public bool RemoveNeighbors(IReadOnlyDictionary<Vertex, IReadOnlyCollection<Vertex>> neighborhoods)
        {
            return Transaction(unitOfWork =>
            {
                var repo = unitOfWork.NeighborsRepository;
                neighborhoods.ForEach(x => x.Value.ForEach(i => repo.DeleteNeighbour(x.Key.Id, i.Id)));
                return true;
            });
        }

        public bool UpdateRange(IEnumerable<(int Order, Vertex Vertex)> vertices, int graphId)
        {
            return Transaction(unitOfWork =>
            {
                foreach (var vertex in vertices)
                {
                    var entity = unitOfWork.RangeRepository.GetByVertexId(vertex.Vertex.Id);
                    entity.Position = vertex.Order;
                    unitOfWork.RangeRepository.Update(entity);
                }
                return true;
            });
        }

        public bool RemoveRange(int graphId)
        {
            return Transaction(unitOfWork => unitOfWork.RangeRepository.DeleteByGraphId(graphId));
        }

        public PathfindingHistoryReadDto GetPathfindingHistory(int graphId)
        {
            return Transaction(unitOfWork => new PathfindingHistoryReadDto()
            {
                Id = graphId,
                Graph = unitOfWork.CreateGraph(graphId, mapper),
                Algorithms = unitOfWork.GetAlgorithms(graphId, mapper),
                Range = unitOfWork.GetRange(graphId)
            });
        }

        public bool UpdateVertices(IEnumerable<Vertex> vertices, int graphId)
        {
            return Transaction(unitOfWork =>
            {
                var entities = mapper.Map<VertexEntity[]>(vertices);
                entities.ForEach(x => x.GraphId = graphId);
                return unitOfWork.VerticesRepository.UpdateVertices(entities);
            });
        }

        public bool UpdateObstaclesCount(int newCount, int graphId)
        {
            return Transaction(unitOfWork =>
            {
                var entity = unitOfWork.GraphRepository.GetGraph(graphId);
                entity.ObstaclesCount = newCount;
                return unitOfWork.GraphRepository.Update(entity);
            });
        }

        public bool AddNeighbors(IReadOnlyDictionary<Vertex, IReadOnlyCollection<Vertex>> neighborhoods)
        {
            return Transaction(unitOfWork =>
            {
                var list = neighborhoods.SelectMany(neighborhood =>
                    neighborhood.Value.Select(neighbor => new NeighborEntity
                    {
                        VertexId = neighborhood.Key.Id,
                        NeighborId = neighbor.Id
                    })).ToReadOnly();
                unitOfWork.NeighborsRepository.AddNeighbours(list);
                return true;
            });
        }

        public bool RemoveRange(IEnumerable<Vertex> vertices, int graphId)
        {
            return Transaction(unitOfWork =>
            {
                vertices.ForEach(x => unitOfWork.RangeRepository.DeleteByVertexId(x.Id));
                return true;
            });
        }

        public IReadOnlyCollection<PathfindingHistoryReadDto> AddPathfindingHistory(IEnumerable<PathfindingHistorySerializationDto> histories)
        {
            var dtos = mapper.Map<PathfindingHistoryCreateDto[]>(histories);
            return AddPathfindingHistory(dtos);
        }

        public PathfindingHistorySerializationDto GetSerializationHistory(int graphId)
        {
            var dto = GetPathfindingHistory(graphId);
            return mapper.Map<PathfindingHistorySerializationDto>(dto);
        }

        public GraphSerializationDto GetSerializationGraph(int graphId)
        {
            var graph = GetGraph(graphId);
            return mapper.Map<GraphSerializationDto>(graph);
        }

        public int AddGraph(GraphSerializationDto graph)
        {
            var add = mapper.Map<IGraph<Vertex>>(graph);
            return AddGraph(add);
        }

        private T Transaction<T>(Func<IUnitOfWork, T> action)
        {
            using (var unitOfWork = factory.Create())
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    var result = action(unitOfWork);
                    unitOfWork.SaveChanges();
                    unitOfWork.CommitTransaction();
                    return result;
                }
                catch (Exception)
                {
                    unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        private IReadOnlyCollection<RangeEntity> SelectRangeEntities(IEnumerable<(int Order, Vertex Vertex)> vertices, int graphId)
        {
            return vertices.Select(x => new RangeEntity
            {
                GraphId = graphId,
                VertexId = x.Vertex.Id,
                Position = x.Order
            }).ToReadOnly();
        }
    }
}