using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.LiteDb;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Service.Interface.Requests.Update;
using Pathfinding.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business
{
    public sealed class RequestService<T> : IRequestService<T>
        where T : IVertex, IEntity<int>
    {
        private readonly IMapper mapper;
        private readonly Func<IUnitOfWork> factory;

        public RequestService(IMapper mapper, IUnitOfWorkFactory factory)
            : this(mapper, factory.Create)
        {
        }

        public RequestService(IMapper mapper)
            : this(mapper, new LiteDbInMemoryUnitOfWorkFactory())
        {
        }

        public RequestService(IMapper mapper, Func<IUnitOfWork> factory)
        {
            this.factory = factory;
            this.mapper = mapper;
        }

        public async Task<bool> CreateNeighborsAsync(CreateNeighborsRequest<T> request, CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var list = request.Neighbors.SelectMany(neighborhood =>
                    neighborhood.Value.Select(neighbor => new Neighbor
                    {
                        GraphId = request.GraphId,
                        VertexId = neighborhood.Key.Id,
                        NeighborId = neighbor.Id
                    })).ToReadOnly();
                await unitOfWork.NeighborsRepository.CreateAsync(list, t);
                return true;
            }, token);
        }

        public async Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> CreatePathfindingHistoriesAsync(IEnumerable<CreatePathfindingHistoryRequest<T>> request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var models = await request.ToAsyncEnumerable()
                    .SelectAwait(async history =>
                    {
                        var graph = history.Graph.Graph;
                        var model = await unitOfWork.AddGraphAsync(mapper, history.Graph, t);
                        history.Algorithms.ForEach(x => x.Run.GraphId = model.Id);
                        var runHistory = await unitOfWork.AddHistoryAsync(mapper, history.Algorithms, t);
                        var vertices = history.Range.Select((x, i) => (Order: i, Vertex: graph.Get(x)));
                        var entities = SelectRangeEntities(vertices, model.Id);
                        await unitOfWork.RangeRepository.CreateAsync(entities, t);
                        return new PathfindingHistoryModel<T>()
                        {
                            Graph = model,
                            Algorithms = runHistory,
                            Range = history.Range
                        };
                    })
                    .ToListAsync(token);
                return models.ToReadOnly();
            }, token);
        }

        public async Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> CreatePathfindingHistoriesAsync(IEnumerable<PathfindingHistorySerializationModel> request,
            CancellationToken token = default)
        {
            var requests = mapper.Map<List<CreatePathfindingHistoryRequest<T>>>(request);
            return await CreatePathfindingHistoriesAsync(requests, token);
        }

        public async Task<bool> CreateRangeAsync(CreatePathfindingRangeRequest<T> request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var list = SelectRangeEntities(request.Vertices, request.GraphId);
                await unitOfWork.RangeRepository.CreateAsync(list, t);
                return true;
            }, token);
        }

        public async Task<IReadOnlyCollection<AlgorithmRunHistoryModel>> CreateRunHistoriesAsync(IEnumerable<CreateAlgorithmRunHistoryRequest> histories,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)=> 
            {
                var result = await unitOfWork.AddHistoryAsync(mapper, histories);
                return mapper.Map<List<AlgorithmRunHistoryModel>>(result);
            }, token);
        }

        public async Task<bool> DeleteGraphAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.GraphRepository.DeleteAsync(graphId, t), token);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<T> request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var verticesIds = request.Select(x => x.Id);
                return await unitOfWork.RangeRepository
                    .DeleteByVerticesIdsAsync(verticesIds, t);
            }, token);
        }

        public async Task<bool> DeleteRangeAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.RangeRepository.DeleteByGraphIdAsync(graphId, t), token);
        }

        public async Task<IReadOnlyCollection<GraphInformationModel>> ReadAllGraphInfoAsync(CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var result = await unitOfWork.GraphRepository.GetAll(t).ToListAsync(token);
                return mapper.Map<List<GraphInformationModel>>(result);
            }, token);
        }

        public async Task<GraphModel<T>> ReadGraphAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.ReadGraphAsync<T>(graphId, mapper, t));
        }

        public async Task<GraphModel<T>> CreateGraphAsync(GraphSerializationModel graph,
            CancellationToken token = default)
        {
            return await CreateGraphAsync(mapper.Map<CreateGraphRequest<T>>(graph), token);
        }

        public async Task<GraphModel<T>> CreateGraphAsync(CreateGraphRequest<T> graph,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.AddGraphAsync<T>(mapper, graph, t), token);
        }

        public async Task<int> ReadGraphCountAsync(CancellationToken token = default)
        {
            using (var unitOfWork = factory())
            {
                return await unitOfWork.GraphRepository.ReadCountAsync(token);
            }
        }

        public async Task<IReadOnlyCollection<int>> ReadGraphIdsAsync(CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                return await unitOfWork.GraphRepository.GetAll()
                    .Select(x => x.Id)
                    .ToListAsync(token);
            }, token);
        }

        public async Task<PathfindingHistoryModel<T>> ReadPathfindingHistoryAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                return new PathfindingHistoryModel<T>()
                {
                    Graph = await unitOfWork.ReadGraphAsync<T>(graphId, mapper, t),
                    Algorithms = await unitOfWork.GetAlgorithmRuns(graphId, mapper, t),
                    Range = await unitOfWork.GetRangeAsync(graphId, mapper, t)
                };
            }, token);
        }

        public async Task<PathfindingRangeModel> ReadRangeAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var range = await unitOfWork.GetRangeAsync(graphId, mapper, t);
                return new PathfindingRangeModel() { Range = range.ToList() };
            }, token);
        }

        public async Task<int> ReadRunCountAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unit, t)
                => await unit.RunRepository.ReadCount(graphId, t), token);
        }

        public async Task<RunVisualizationModel> ReadRunInfoAsync(int runId,
            CancellationToken token = default)
        {
            return await Transaction(async (unit, t) =>
            {
                var graphState = await unit.GraphStateRepository.ReadByRunIdAsync(runId, t);
                var subAlgorithms = await unit.SubAlgorithmRepository.ReadByAlgorithmRunIdAsync(runId, t);
                var speed = await unit.StatisticsRepository.ReadByAlgorithmRunIdAsync(runId, t);
                return new RunVisualizationModel()
                {
                    GraphState = mapper.Map<GraphStateModel>(graphState),
                    Algorithms = mapper.Map<SubAlgorithmModel[]>(subAlgorithms).ToReadOnly(),
                    AlgorithmSpeed = speed.AlgorithmSpeed
                };
            }, token);
        }

        public async Task<IReadOnlyCollection<RunStatisticsModel>> ReadRunStatisticsAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unit, t) =>
            {
                var runs = (await unit.RunRepository
                    .ReadByGraphIdAsync(graphId, t))
                    .OrderBy(x => x.Id)
                    .ToReadOnly();
                var statistics = (await unit.StatisticsRepository
                    .ReadByRunIdsAsync(runs.Select(x => x.Id), t))
                    .OrderBy(x => x.AlgorithmRunId)
                    .ToReadOnly();
                return mapper.Map<RunStatisticsModel[]>(statistics)
                    .ForEach((x, i) => x.AlgorithmId = runs[i].AlgorithmId)
                    .ToList();
            });
        }

        public async Task<GraphSerializationModel> ReadSerializationGraphAsync(int graphId,
            CancellationToken token = default)
        {
            return mapper.Map<GraphSerializationModel>(await ReadGraphAsync(graphId, token));
        }

        public async Task<PathfindingHistorySerializationModel> ReadSerializationHistoryAsync(int graphId,
            CancellationToken token = default)
        {
            return mapper.Map<PathfindingHistorySerializationModel>(await ReadPathfindingHistoryAsync(graphId, token));
        }

        public async Task<bool> RemoveNeighborsAsync(IReadOnlyDictionary<T, IReadOnlyCollection<T>> request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var data = request
                    .Select(x => (VertexId: x.Key.Id, Neighbors: x.Value.Select(x => x.Id).ToList()))
                    .ToReadOnly();
                return await unitOfWork.NeighborsRepository.DeleteNeighboursAsync(data, t);
            }, token);
        }

        public async Task<bool> UpdateObstaclesCountAsync(UpdateGraphInfoRequest request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var entity = await unitOfWork.GraphRepository.ReadAsync(request.Id, t);
                entity.ObstaclesCount = request.ObstaclesCount;
                return await unitOfWork.GraphRepository.UpdateAsync(entity, t);
            }, token);
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<(int Order, T Vertex)> request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var verts = request.Select(x => x.Vertex.Id).ToReadOnly();
                var ranges = (await unitOfWork.RangeRepository.ReadByVerticesIdsAsync(verts, t))
                    .OrderBy(x => x.VertexId).ToReadOnly();
                var orderedVertices = request.OrderBy(x => x.Vertex.Id).ToReadOnly();
                ranges.ForEach((x, i) => x.Order = orderedVertices[i].Order);
                return await unitOfWork.RangeRepository.UpdateAsync(ranges, t);
            }, token);
        }

        public async Task<bool> UpdateVerticesAsync(UpdateVerticesRequest<T> request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var entities = mapper.Map<Vertex[]>(request.Vertices);
                entities.ForEach(x => x.GraphId = request.GraphId);
                return await unitOfWork.VerticesRepository.UpdateVerticesAsync(entities, t);
            }, token);
        }

        public async Task<bool> DeleteGraphsAsync(IEnumerable<int> ids,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.GraphRepository.DeleteAsync(ids, t), token);
        }

        public async Task<IReadOnlyCollection<GraphModel<T>>> CreateGraphsAsync(IEnumerable<GraphSerializationModel> request,
            CancellationToken token = default)
        {
            return await request
                .ToAsyncEnumerable()
                .SelectAwait(async x => await CreateGraphAsync(x, token))
                .ToListAsync(token);
        }

        private IReadOnlyCollection<PathfindingRange> SelectRangeEntities(IEnumerable<(int Order, T Vertex)> vertices, int graphId)
        {
            return vertices.Select(x => new PathfindingRange
            {
                GraphId = graphId,
                VertexId = x.Vertex.Id,
                Order = x.Order
            }).ToReadOnly();
        }

        private async Task<TParam> Transaction<TParam>(Func<IUnitOfWork, CancellationToken, Task<TParam>> action,
            CancellationToken token = default)
        {
            using (var unitOfWork = factory())
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    var result = await action(unitOfWork, token);
                    await unitOfWork.CommitAsync(token);
                    return result;
                }
                catch (Exception)
                {
                    await unitOfWork.RollbackAsync(token);
                    throw;
                }
            }
        }
    }
}
