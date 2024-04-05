using AutoMapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.DAL.UOF.Factories;
using Pathfinding.App.Console.Extensions;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Services
{
    internal sealed class Service<T>(IMapper mapper,
        IUnitOfWorkFactory factory = null) : IService<T>
        where T : IVertex
    {
        private readonly IMapper mapper = mapper;
        private readonly IUnitOfWorkFactory factory = factory ?? new LiteDbInMemoryUnitOfWorkFactory();

        public GraphReadDto<T> AddGraph(GraphCreateDto<T> graph)
        {
            return Transaction(unitOfWork => unitOfWork.AddGraph(mapper, graph));
        }

        public IReadOnlyCollection<PathfindingHistoryReadDto<T>> AddPathfindingHistory(IEnumerable<PathfindingHistoryCreateDto<T>> histories)
        {
            return Transaction(unitOfWork => histories.Select(history =>
            {
                var dto = unitOfWork.AddGraph(mapper, history.Graph);
                history.Algorithms.ForEach(x => x.Run.GraphId = dto.Id);
                unitOfWork.AddHistory(mapper, history.Algorithms);
                var vertices = history.Range
                    .Select((x, i) => (Order: i, Vertex: history.Graph.Graph.Get(x)));
                var entities = SelectRangeEntities(vertices, dto.Id);
                unitOfWork.RangeRepository.Insert(entities);
                return mapper.Map<PathfindingHistoryReadDto<T>>(history) with { Graph = dto };
            }).ToReadOnly());
        }

        public bool AddRange(IEnumerable<(int Order, T Vertex)> vertices, int graphId)
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

        public IReadOnlyList<GraphInformationReadDto> GetAllGraphInfo()
        {
            return Transaction(unitOfWork =>
            {
                var result = unitOfWork.GraphRepository.GetAll();
                return mapper.Map<GraphInformationReadDto[]>(result);
            });
        }

        public GraphReadDto<T> GetGraph(int id)
        {
            return Transaction(unitOfWork => unitOfWork.CreateGraph<T>(id, mapper));
        }

        public IReadOnlyCollection<int> GetGraphIds()
        {
            return Transaction(unitOfWork => unitOfWork.GraphRepository.GetAll().Select(x => x.Id).ToReadOnly());
        }

        public IReadOnlyCollection<ICoordinate> GetRange(int graphId)
        {
            return Transaction(unitOfWork => unitOfWork.GetRange(graphId));
        }

        public bool RemoveNeighbors(IReadOnlyDictionary<T, IReadOnlyCollection<T>> neighborhoods)
        {
            return Transaction(unitOfWork =>
            {
                var repo = unitOfWork.NeighborsRepository;
                neighborhoods.ForEach(x => x.Value.ForEach(i => repo.DeleteNeighbour(((dynamic)x.Key).Id, ((dynamic)i).Id)));
                return true;
            });
        }

        public bool UpdateRange(IEnumerable<(int Order, T Vertex)> vertices, int graphId)
        {
            return Transaction(unitOfWork =>
            {
                var verts = vertices.Select(x => (int)((dynamic)x.Vertex).Id).ToReadOnly();
                var ranges = unitOfWork.RangeRepository.GetByVerticesIds(verts)
                    .OrderBy(x => x.VertexId).ToReadOnly();
                var orderedVertices = vertices.OrderBy(x => (int)((dynamic)x.Vertex).Id).ToReadOnly();
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

        public PathfindingHistoryReadDto<T> GetPathfindingHistory(int graphId)
        {
            return Transaction(unitOfWork => new PathfindingHistoryReadDto<T>()
            {
                Graph = unitOfWork.CreateGraph<T>(graphId, mapper),
                Algorithms = unitOfWork.GetAlgorithmRuns(graphId, mapper),
                Range = unitOfWork.GetRange(graphId)
            });
        }

        public bool UpdateVertices(IEnumerable<T> vertices, int graphId)
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

        public bool AddNeighbors(IReadOnlyDictionary<T, IReadOnlyCollection<T>> neighborhoods)
        {
            return Transaction(unitOfWork =>
            {
                var list = neighborhoods.SelectMany(neighborhood =>
                    neighborhood.Value.Select(neighbor => new NeighborEntity
                    {
                        VertexId = ((dynamic)neighborhood.Key).Id,
                        NeighborId = ((dynamic)neighbor).Id
                    })).ToReadOnly();
                unitOfWork.NeighborsRepository.Insert(list);
                return true;
            });
        }

        public bool RemoveRange(IEnumerable<T> vertices, int graphId)
        {
            return Transaction(unitOfWork =>
            {
                vertices.ForEach(x => unitOfWork.RangeRepository.DeleteByVertexId(((dynamic)x).Id));
                return true;
            });
        }

        public IReadOnlyCollection<PathfindingHistoryReadDto<T>> AddPathfindingHistory(IEnumerable<PathfindingHistorySerializationDto> histories)
        {
            return AddPathfindingHistory(mapper.Map<PathfindingHistoryCreateDto<T>[]>(histories));
        }

        public PathfindingHistorySerializationDto GetSerializationHistory(int graphId)
        {
            return mapper.Map<PathfindingHistorySerializationDto>(GetPathfindingHistory(graphId));
        }

        public GraphSerializationDto GetSerializationGraph(int graphId)
        {
            return mapper.Map<GraphSerializationDto>(GetGraph(graphId));
        }

        public GraphReadDto<T> AddGraph(GraphSerializationDto graph)
        {
            return AddGraph(mapper.Map<GraphCreateDto<T>>(graph));
        }

        private IReadOnlyCollection<RangeEntity> SelectRangeEntities(IEnumerable<(int Order, T Vertex)> vertices, int graphId)
        {
            return vertices.Select(x => new RangeEntity
            {
                GraphId = graphId,
                VertexId = ((dynamic)x.Vertex).Id,
                Order = x.Order
            }).ToReadOnly();
        }

        public IReadOnlyCollection<RunStatisticsDto> GetRunStatisticsForGraph(int graphId)
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
                return mapper.Map<RunStatisticsDto[]>(statistics)
                    .ForEach((x, i) => x.AlgorithmId = runs[i].AlgorithmId)
                    .ToReadOnly();
            });
        }

        public RunVisualizationDto GetRunInfo(int runId)
        {
            return Transaction(unit =>
            {
                var graphState = unit.GraphStateRepository.GetByRunId(runId);
                var subAlgorithms = unit.SubAlgorithmRepository.GetByAlgorithmRunId(runId);
                var speed = unit.StatisticsRepository.GetByAlgorithmRunId(runId).AlgorithmSpeed;
                return new RunVisualizationDto()
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
            return Transaction(unit => unit.RunRepository.GetCount(graphId));
        }

        public int GetGraphCount()
        {
            return Transaction(unit => unit.GraphRepository.GetCount());
        }

        private TParam Transaction<TParam>(Func<IUnitOfWork, TParam> action)
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