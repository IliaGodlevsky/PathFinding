using AutoMapper;
using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Services
{
    internal sealed class Service : IService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IVertexFactory<Vertex> vertexFactory;
        private readonly IGraphFactory<Vertex> graphFactory;
        private readonly IMapper mapper;

        public Service(IUnitOfWork unitOfWork, 
            IMapper mapper,
            IVertexFactory<Vertex> vertexFactory,
            IGraphFactory<Vertex> graphFactory)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.vertexFactory = vertexFactory;
            this.graphFactory = graphFactory;
        }

        public int AddAlgorithm(AlgorithmCreateDto algorithm)
        {
            return Transaction<int>(() =>
            {
                var entity = mapper.Map<AlgorithmEntity>(algorithm);
                unitOfWork.AlgorithmsRepository.AddOne(entity);
                return entity.Id;
            });
        }

        public int AddGraph(IGraph<Vertex> graph)
        {
            return Transaction<int>(() => unitOfWork.AddGraph(mapper, graph));
        }

        public bool AddNeighbor(Vertex vertex, Vertex neighbor)
        {
            return Transaction<bool>(() =>
            {
                var entity = new NeighbourEntity
                {
                    VertexId = vertex.Id,
                    NeighbourId = neighbor.Id
                };
                unitOfWork.NeighborsRepository.AddNeighbour(entity);
                return entity.Id > 0;
            });
        }

        public int AddPathfindingHistory(PathfindingHistoryCreateDto history)
        {
            return Transaction<int>(() =>
            {
                int id = unitOfWork.AddGraph(mapper, history.Graph);
                var algorithms = mapper.Map<AlgorithmEntity[]>(history.Algorithms);
                algorithms.ForEach(a => a.GraphId = id);
                unitOfWork.AlgorithmsRepository.AddMany(algorithms);
                unitOfWork.AddRange(history.Graph, history.Range, id);
                return id;
            });
        }

        public bool AddRange(Vertex vertex, int order, int graphId)
        {
            return Transaction<bool>(() =>
            {
                var entity = new RangeEntity
                {
                    GraphId = graphId,
                    VertexId = vertex.Id,
                    Order = order
                };
                unitOfWork.RangeRepository.AddRange(entity);
                return true;
            });
        }

        public bool DeleteGraph(int graphId)
        {
            return Transaction<bool>(() => unitOfWork.DeleteGraph(graphId));
        }

        public IReadOnlyList<GraphEntity> GetAllGraphInfo()
        {
            return Array.AsReadOnly(unitOfWork.GraphRepository.GetAll().ToArray());
        }

        public IGraph<Vertex> GetGraph(int id)
        {
            return unitOfWork.CreateGraph(id, mapper, graphFactory);
        }

        public IReadOnlyCollection<int> GetGraphIds()
        {
            return Array.AsReadOnly(unitOfWork.GraphRepository
                .GetAll().Select(x => x.Id).ToArray());
        }

        public IReadOnlyCollection<AlgorithmReadDto> GetGraphPathfindingHistory(int graphId)
        {
            var entities = unitOfWork.AlgorithmsRepository.GetByGraphId(graphId);
            return Array.AsReadOnly(mapper.Map<IEnumerable<AlgorithmEntity>, AlgorithmReadDto[]>(entities).ToArray());
        }

        public IReadOnlyCollection<ICoordinate> GetRange(int graphId)
        {
            return unitOfWork.GetRange(graphId);
        }

        public bool RemoveNeighbor(Vertex vertex, Vertex neighbor)
        {
            return Transaction<bool>(() => unitOfWork.NeighborsRepository.DeleteNeighbour(vertex.Id, neighbor.Id));
        }

        public bool RemoveRange(Vertex vertex, int graphId)
        {
            return Transaction<bool>(() => unitOfWork.RangeRepository.DeleteByVertexId(vertex.Id));
        }

        public bool RemoveRange(int graphId)
        {
            return Transaction<bool>(() => unitOfWork.RangeRepository.DeleteByGraphId(graphId)); ;
        }

        public bool UpdateRange(Vertex vertex, int order, int graphId)
        {
            return Transaction<bool>(() =>
            {
                var entity = unitOfWork.RangeRepository.GetByVertexId(vertex.Id);
                entity.Order = order;
                return unitOfWork.RangeRepository.Update(entity);
            });
        }

        public PathfindingHistoryReadDto GetPathfindingHistory(int graphId)
        {
            var graph = unitOfWork.CreateGraph(graphId, mapper, graphFactory);
            var algorithmEntities = unitOfWork.AlgorithmsRepository.GetByGraphId(graphId);
            var range = unitOfWork.GetRange(graphId);
            return new PathfindingHistoryReadDto()
            {
                Id = graphId,
                Graph = graph,
                Algorithms = mapper.Map<AlgorithmReadDto[]>(algorithmEntities),
                Range = range
            };
        }

        public bool UpdateVertex(Vertex vertex, int graphId)
        {
            return Transaction<bool>(() =>
            {
                var entity = mapper.Map<VertexEntity>(vertex);
                entity.GraphId = graphId;
                return unitOfWork.VerticesRepository.UpdateVertex(entity);
            });
        }

        public bool UpdateVertices(IEnumerable<Vertex> vertices, int graphId)
        {
            return Transaction<bool>(() =>
            {
                var entities = mapper.Map<VertexEntity[]>(vertices);
                entities.ForEach(x => x.GraphId = graphId);
                return unitOfWork.VerticesRepository.UpdateVertices(entities);
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
                return default;
            }
        }

        public bool UpdateObstaclesCount(int newCount, int graphId)
        {
            return Transaction<bool>(() =>
            {
                var entity = unitOfWork.GraphRepository.GetGraph(graphId);
                entity.ObstaclesCount = newCount;
                return unitOfWork.GraphRepository.Update(entity);
            });
        }
    }
}
