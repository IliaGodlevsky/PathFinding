using AutoMapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
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
            this.factory = factory ?? new LiteDbInMemoryUnitOfWorkFactory();
        }

        public AlgorithmRunReadDto AddAlgorithm(AlgorithmRunCreateDto run)
        {
            return Transaction(unit =>
            {
                var entity = mapper.Map<AlgorithmRunEntity>(run);
                unit.RunRepository.Insert(entity);
                return mapper.Map<AlgorithmRunReadDto>(entity);
            });
        }

        public GraphReadDto AddGraph(IGraph<Vertex> dto)
        {
            return Transaction(unitOfWork => unitOfWork.AddGraph(mapper, dto));
        }

        public IReadOnlyCollection<PathfindingHistoryReadDto> AddPathfindingHistory(IEnumerable<PathfindingHistoryCreateDto> histories)
        {
            return Transaction(unitOfWork => histories.Select(history =>
            {
                var dto = unitOfWork.AddGraph(mapper, history.Graph);
                history.Algorithms.ForEach(x => x.Run.GraphId = dto.Id);
                unitOfWork.AddHistory(mapper, history.Algorithms);
                var vertices = history.Range
                    .Select((x, i) => (Order: i, Vertex: history.Graph.Get(x)));
                var entities = SelectRangeEntities(vertices, dto.Id);
                unitOfWork.RangeRepository.Insert(entities);
                return mapper.Map<PathfindingHistoryReadDto>(history) with { Id = dto.Id };
            }).ToReadOnly());
        }

        public bool AddRange(IEnumerable<(int Order, Vertex Vertex)> vertices, int graphId)
        {
            return Transaction(unitOfWork =>
            {
                var list = SelectRangeEntities(vertices, graphId);
                unitOfWork.RangeRepository.Insert(list);
                return true;
            });
        }

        public bool DeleteGraph(int graphId)
        {
            return Transaction(unitOfWork => unitOfWork.GraphRepository.Delete(graphId));
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
            return Transaction(unitOfWork => unitOfWork.GraphRepository
                                .GetAll().Select(x => x.Id).ToReadOnly());
        }

        public IReadOnlyCollection<AlgorithmRunHistoryReadDto> GetGraphPathfindingHistory(int graphId)
        {
            return Transaction(unitOfWork => unitOfWork.GetAlgorithmRuns(graphId, mapper));
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
                var verts = vertices.Select(x => x.Vertex.Id).ToReadOnly();
                var ranges = unitOfWork.RangeRepository.GetByVerticesIds(verts)
                    .OrderBy(x => x.VertexId).ToReadOnly();
                var orderedVertices = vertices.OrderBy(x => x.Vertex.Id).ToReadOnly();
                for (int i = 0; i < ranges.Count; i++)
                {
                    ranges[i].Order = orderedVertices[i].Order;
                }
                return unitOfWork.RangeRepository.Update(ranges);
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
                Algorithms = unitOfWork.GetAlgorithmRuns(graphId, mapper),
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
                var entity = unitOfWork.GraphRepository.Read(graphId);
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
                unitOfWork.NeighborsRepository.Insert(list);
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

        public GraphReadDto AddGraph(GraphSerializationDto graph)
        {
            var add = mapper.Map<IGraph<Vertex>>(graph);
            return AddGraph(add);
        }

        private IReadOnlyCollection<RangeEntity> SelectRangeEntities(IEnumerable<(int Order, Vertex Vertex)> vertices, int graphId)
        {
            return vertices.Select(x => new RangeEntity
            {
                GraphId = graphId,
                VertexId = x.Vertex.Id,
                Order = x.Order
            }).ToReadOnly();
        }

        public IReadOnlyCollection<RunStatisticsDto> GetRunStatiticsForGraph(int graphId)
        {
            return Transaction(unit =>
            {
                var runs = unit.RunRepository
                    .GetByGraphId(graphId)
                    .OrderBy(x => x.Id)
                    .ToReadOnly();
                var statistics = unit.StatisticsRepository
                    .GetByRunIds(runs.Select(x => x.Id))
                    .OrderBy(x => x.AlgorithmRunId)
                    .ToReadOnly();
                var runStatistics = mapper.Map<RunStatisticsDto[]>(statistics).ToReadOnly();
                for (int i = 0; i < runs.Count; i++)
                {
                    runStatistics[i].AlgorithmId = runs[i].AlgorithmId;
                }
                return runStatistics;
            });
        }

        public RunVisualizationDto GetRunInfo(int runId)
        {
            return Transaction<RunVisualizationDto>(unit =>
            {
                var graphState = unit.GraphStateRepository.GetByRunId(runId);
                var subAlgorithms = unit.SubAlgorithmRepository.GetByAlgorithmRunId(runId);
                var speed = unit.StatisticsRepository.GetByAlgorithmRunId(runId).AlgorithmSpeed;
                return new()
                {
                    GraphState = mapper.Map<GraphStateReadDto>(graphState),
                    Algorithms = mapper.Map<SubAlgorithmReadDto[]>(subAlgorithms).ToReadOnly(),
                    AlgorithmSpeed = speed
                };
            });
        }

        public void AddRunHistory(params AlgorithmRunHistoryCreateDto[] histories)
        {
            Transaction(unit =>
            {
                unit.AddHistory(mapper, histories);
                return true;
            });
        }

        public int GetRunCount(int graphId)
        {
            return Transaction<int>(unit => unit.RunRepository.GetCount(graphId));
        }

        public int GetGraphCount()
        {
            return Transaction(unit => unit.GraphRepository.GetCount());
        }

        private T Transaction<T>(Func<IUnitOfWork, T> action)
        {
            using (var unitOfWork = factory.Create())
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    var result = action(unitOfWork);
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