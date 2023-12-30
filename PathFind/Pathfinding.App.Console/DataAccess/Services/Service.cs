using AutoMapper;
using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.DataAccess.UnitOfWorks.Factories;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Services
{
    internal class Service : IService
    {
        protected readonly IMapper mapper;
        protected readonly IUnitOfWorkFactory factory;

        public Service(IMapper mapper, IUnitOfWorkFactory factory = null)
        {
            this.mapper = mapper;
            this.factory = factory ?? new InMemoryUnitOfWorkFactory();
        }

        public Service(Service  service) 
            : this(service.mapper, service.factory)
        {
        }

        public virtual int AddAlgorithm(AlgorithmCreateDto algorithm)
        {
            return Transaction<int>(unitOfWork =>
            {
                var entity = mapper.Map<AlgorithmEntity>(algorithm);
                unitOfWork.AlgorithmsRepository.AddOne(entity);
                return entity.Id;
            });
        }

        public virtual int AddGraph(IGraph<Vertex> dto)
        {
            return Transaction<int>(unitOfWork => unitOfWork.AddGraph(mapper, dto));
        }

        public virtual PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistoryCreateDto history)
        {
            return Transaction<PathfindingHistoryReadDto>(unitOfWork =>
            {
                var graph = history.Graph;
                int id = unitOfWork.AddGraph(mapper, graph);
                var algorithms = mapper.Map<AlgorithmEntity[]>(history.Algorithms);
                algorithms.ForEach(a => a.GraphId = id);
                unitOfWork.AlgorithmsRepository.AddMany(algorithms);
                var entities = history.Range
                    .Select(graph.Get)
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

        public virtual bool AddRange((int Order, Vertex Vertex)[] vertices, int graphId)
        {
            return Transaction<bool>(unitOfWork =>
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

        public virtual bool DeleteGraph(int graphId)
        {
            return Transaction<bool>(unitOfWork =>
            {
                unitOfWork.NeighborsRepository.DeleteByGraphId(graphId);
                unitOfWork.GraphRepository.DeleteGraph(graphId);
                unitOfWork.AlgorithmsRepository.DeleteByGraphId(graphId);
                unitOfWork.VerticesRepository.DeleteVerticesByGraphId(graphId);
                unitOfWork.RangeRepository.DeleteByGraphId(graphId);
                return true;
            });
        }

        public virtual IReadOnlyList<GraphEntity> GetAllGraphInfo()
        {
            return Transaction<IReadOnlyList<GraphEntity>>(
                unitOfWork => unitOfWork.GraphRepository.GetAll().ToList().AsReadOnly());
        }

        public virtual IGraph<Vertex> GetGraph(int id)
        {
            return Transaction<IGraph<Vertex>>(unitOfWork =>
            {
                var graphEntity = unitOfWork.GraphRepository.GetGraph(id);
                var vertexEntities = unitOfWork.VerticesRepository
                    .GetVerticesByGraphId(id)
                    .ToDictionary(x => x.Id);
                var ids = vertexEntities.Select(x => x.Key).ToArray();
                var neighbors = unitOfWork.NeighborsRepository
                    .GetNeighboursForVertices(ids)
                    .ToDictionary(x => x.Key, x => x.Value.Select(i => vertexEntities[i.NeighborId]));
                return unitOfWork.CreateGraph(id, mapper);
            });
        }

        public virtual IReadOnlyCollection<int> GetGraphIds()
        {
            return Transaction<IReadOnlyCollection<int>>(unitOfWork =>
            {
                return unitOfWork.GraphRepository.GetAll().Select(x => x.Id).ToReadOnly();
            });
        }

        public virtual IReadOnlyCollection<AlgorithmReadDto> GetGraphPathfindingHistory(int graphId)
        {
            return Transaction<IReadOnlyCollection<AlgorithmReadDto>>(
                unitOfWork => unitOfWork.GetAlgorithms(graphId, mapper));
        }

        public virtual IReadOnlyCollection<ICoordinate> GetRange(int graphId)
        {
            return Transaction<IReadOnlyCollection<ICoordinate>>(
                unitOfWork => unitOfWork.GetRange(graphId));
        }

        public virtual bool RemoveNeighbors(IReadOnlyDictionary<int, int[]> neighborhoods)
        {
            return Transaction<bool>(unitOfWork =>
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

        public virtual bool UpdateRange((int Order, Vertex Vertex)[] vertices, int graphId)
        {
            return Transaction<bool>(unitOfWork =>
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

        public virtual bool RemoveRange(int graphId)
        {
            return Transaction<bool>(unitOfWork 
                => unitOfWork.RangeRepository.DeleteByGraphId(graphId)); 
        }

        public virtual PathfindingHistoryReadDto GetPathfindingHistory(int graphId)
        {
            return Transaction<PathfindingHistoryReadDto>(unitOfWork =>
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

        public virtual bool UpdateVertices(IEnumerable<Vertex> vertices, int graphId)
        {
            return Transaction<bool>(unitOfWork =>
            {
                var entities = mapper.Map<VertexEntity[]>(vertices);
                entities.ForEach(x => x.GraphId = graphId);
                return unitOfWork.VerticesRepository.UpdateVertices(entities);
            });
        }

        public virtual bool UpdateObstaclesCount(int newCount, int graphId)
        {
            return Transaction<bool>(unitOfWork =>
            {
                var entity = unitOfWork.GraphRepository.GetGraph(graphId);
                entity.ObstaclesCount = newCount;
                return unitOfWork.GraphRepository.Update(entity);
            });
        }

        public virtual bool AddNeighbors(IReadOnlyDictionary<int, int[]> neighborhoods)
        {
            return Transaction<bool>(unitOfWork =>
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

        public virtual bool RemoveRange(IEnumerable<Vertex> vertices, int graphId)
        {
            return Transaction<bool>(unitOfWork =>
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
    }
}