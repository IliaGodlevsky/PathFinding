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

        public PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistoryCreateDto history)
        {
            return Transaction(unitOfWork =>
            {
                int id = unitOfWork.AddGraph(mapper, history.Graph);
                var algorithms = mapper.Map<AlgorithmEntity[]>(history.Algorithms);
                algorithms.ForEach(a => a.GraphId = id);
                unitOfWork.AlgorithmsRepository.AddMany(algorithms);
                var entities = history.Range
                    .Select(history.Graph.Get)
                    .Select((x, i) => new RangeEntity
                    {
                        VertexId = x.Id,
                        GraphId = id,
                        Position = i
                    });
                unitOfWork.RangeRepository.AddRange(entities);
                var read = mapper.Map<PathfindingHistoryReadDto>(history);
                read.Id = id;
                return read;
            });
        }

        public bool AddRange((int Order, Vertex Vertex)[] vertices, int graphId)
        {
            return Transaction(unitOfWork =>
            {
                var list = new List<RangeEntity>();
                foreach (var vertex in vertices)
                {
                    var entity = new RangeEntity
                    {
                        GraphId = graphId,
                        VertexId = vertex.Vertex.Id,
                        Position = vertex.Order
                    };
                    list.Add(entity);
                }
                unitOfWork.RangeRepository.AddRange(list);
                return true;
            });
        }

        public bool DeleteGraph(int graphId)
        {
            return Transaction(unitOfWork =>
            {
                unitOfWork.NeighborsRepository.DeleteByGraphId(graphId);
                unitOfWork.GraphRepository.DeleteGraph(graphId);
                unitOfWork.AlgorithmsRepository.DeleteByGraphId(graphId);
                unitOfWork.VerticesRepository.DeleteVerticesByGraphId(graphId);
                unitOfWork.RangeRepository.DeleteByGraphId(graphId);
                return true;
            });
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

        public bool RemoveNeighbors(IReadOnlyDictionary<int, int[]> neighborhoods)
        {
            return Transaction(unitOfWork =>
            {
                foreach (var neighborhood in neighborhoods)
                {
                    foreach (var neighbor in neighborhoods[neighborhood.Key])
                    {
                        unitOfWork.NeighborsRepository.DeleteNeighbour(neighborhood.Key, neighbor);
                    }
                }
                return true;
            });
        }

        public bool UpdateRange((int Order, Vertex Vertex)[] vertices, int graphId)
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
            return Transaction(unitOfWork
                => unitOfWork.RangeRepository.DeleteByGraphId(graphId));
        }

        public PathfindingHistoryReadDto GetPathfindingHistory(int graphId)
        {
            return Transaction(unitOfWork =>
            {
                var graph = unitOfWork.CreateGraph(graphId, mapper);
                var algorithms = unitOfWork.GetAlgorithms(graphId, mapper);
                var range = unitOfWork.GetRange(graphId);
                return new PathfindingHistoryReadDto()
                {
                    Id = graphId,
                    Graph = graph,
                    Algorithms = algorithms,
                    Range = range
                };
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

        public bool AddNeighbors(IReadOnlyDictionary<int, int[]> neighborhoods)
        {
            return Transaction(unitOfWork =>
            {
                var list = new List<NeighborEntity>();
                foreach (var neighborhood in neighborhoods)
                {
                    foreach (var neighbor in neighborhoods[neighborhood.Key])
                    {
                        var entity = new NeighborEntity()
                        {
                            VertexId = neighborhood.Key,
                            NeighborId = neighbor
                        };
                        list.Add(entity);
                    }
                }
                unitOfWork.NeighborsRepository.AddNeighbours(list);
                return true;
            });
        }

        public bool RemoveRange(IEnumerable<Vertex> vertices, int graphId)
        {
            return Transaction(unitOfWork =>
            {
                foreach (var vertex in vertices)
                {
                    unitOfWork.RangeRepository.DeleteByVertexId(vertex.Id);
                }
                return true;
            });
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

        public PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistorySerializationDto history)
        {
            var createDto = mapper.Map<PathfindingHistoryCreateDto>(history);
            return AddPathfindingHistory(createDto);
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
    }
}