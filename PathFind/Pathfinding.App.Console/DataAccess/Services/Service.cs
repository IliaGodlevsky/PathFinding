using AutoMapper;
using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
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
        protected readonly IUnitOfWork unitOfWork;

        public Service(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? new InMemoryUnitOfWork();
            this.mapper = mapper;
        }

        public Service(Service  service): this(service.unitOfWork, service.mapper)
        {
        }

        public virtual int AddAlgorithm(AlgorithmCreateDto algorithm)
        {
            return Transaction<int>(() =>
            {
                var entity = mapper.Map<AlgorithmEntity>(algorithm);
                unitOfWork.AlgorithmsRepository.AddOne(entity);
                return entity.Id;
            });
        }

        public virtual int AddGraph(IGraph<Vertex> dto)
        {
            return Transaction<int>(() => unitOfWork.AddGraph(mapper, dto));
        }

        public virtual bool AddNeighbor(Vertex vertex, Vertex neighbor)
        {
            return Transaction<bool>(() =>
            {
                var addedEntity = new NeighborEntity
                {
                    VertexId = vertex.Id,
                    NeighborId = neighbor.Id
                };
                unitOfWork.NeighborsRepository.AddNeighbour(addedEntity);
                return addedEntity.Id > 0;
            });
        }

        public virtual PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistoryCreateDto history)
        {
            return Transaction<PathfindingHistoryReadDto>(() =>
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

        public virtual bool AddRange(Vertex vertex, int order, int graphId)
        {
            return Transaction<bool>(() =>
            {
                var entity = new RangeEntity
                {
                    GraphId = graphId,
                    VertexId = vertex.Id,
                    Position = order
                };
                unitOfWork.RangeRepository.AddRange(entity);
                return true;
            });
        }

        public virtual bool DeleteGraph(int graphId)
        {
            return Transaction<bool>(() =>
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
            return unitOfWork.GraphRepository.GetAll().ToList().AsReadOnly();
        }

        public virtual IGraph<Vertex> GetGraph(int id)
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
        }

        public virtual IReadOnlyCollection<int> GetGraphIds()
        {
            return unitOfWork.GraphRepository.GetAll()
                .Select(x => x.Id).ToReadOnly();
        }

        public virtual IReadOnlyCollection<AlgorithmReadDto> GetGraphPathfindingHistory(int graphId)
        {
            return unitOfWork.GetAlgorithms(graphId, mapper);
        }

        public virtual IReadOnlyCollection<ICoordinate> GetRange(int graphId)
        {
            return unitOfWork.GetRange(graphId);
        }

        public virtual bool RemoveNeighbor(Vertex vertex, Vertex neighbor)
        {
            return Transaction<bool>(() => unitOfWork.NeighborsRepository.DeleteNeighbour(vertex.Id, neighbor.Id));
        }

        public virtual bool RemoveRange(Vertex vertex, int graphId)
        {
            return Transaction<bool>(() => unitOfWork.RangeRepository.DeleteByVertexId(vertex.Id));
        }

        public virtual bool RemoveRange(int graphId)
        {
            return Transaction<bool>(() => unitOfWork.RangeRepository.DeleteByGraphId(graphId)); ;
        }

        public virtual bool UpdateRange(Vertex vertex, int order, int graphId)
        {
            return Transaction<bool>(() =>
            {
                var entity = unitOfWork.RangeRepository.GetByVertexId(vertex.Id);
                entity.Position = order;
                return unitOfWork.RangeRepository.Update(entity);
            });
        }

        public virtual PathfindingHistoryReadDto GetPathfindingHistory(int graphId)
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
        }

        public virtual bool UpdateVertex(Vertex vertex, int graphId)
        {
            return Transaction<bool>(() =>
            {
                var entity = mapper.Map<VertexEntity>(vertex);
                entity.GraphId = graphId;
                return unitOfWork.VerticesRepository.UpdateVertex(entity);
            });
        }

        public virtual bool UpdateVertices(IEnumerable<Vertex> vertices, int graphId)
        {
            return Transaction<bool>(() =>
            {
                var entities = mapper.Map<VertexEntity[]>(vertices);
                entities.ForEach(x => x.GraphId = graphId);
                return unitOfWork.VerticesRepository.UpdateVertices(entities);
            });
        }

        public virtual bool UpdateObstaclesCount(int newCount, int graphId)
        {
            return Transaction<bool>(() =>
            {
                var entity = unitOfWork.GraphRepository.GetGraph(graphId);
                entity.ObstaclesCount = newCount;
                return unitOfWork.GraphRepository.Update(entity);
            });
        }

        private T Transaction<T>(Func<T> action)
        {
            try
            {
                unitOfWork.BeginTransaction();
                var result = action();
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