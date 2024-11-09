using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.InMemory;
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
            : this(mapper, new InMemoryUnitOfWorkFactory())
        {
        }

        public RequestService(IMapper mapper, Func<IUnitOfWork> factory)
        {
            this.factory = factory;
            this.mapper = mapper;
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
                        model.IsReadOnly = history.Statistics.Count > 0;
                        var statistics = mapper.Map<List<Statistics>>(history.Statistics);
                        statistics.ForEach(x => x.GraphId = model.Id);
                        await unitOfWork.StatisticsRepository.CreateAsync(statistics, token);
                        var vertices = history.Range.Select((x, i) => (Order: i, Vertex: graph.Get(x)));
                        var entities = SelectRangeEntities(vertices, model.Id);
                        await unitOfWork.RangeRepository.CreateAsync(entities, t);
                        return new PathfindingHistoryModel<T>()
                        {
                            Graph = model,
                            Statistics = mapper.Map<List<RunStatisticsModel>>(statistics),
                            Range = history.Range
                        };
                    })
                    .ToListAsync(token).ConfigureAwait(false);
                return models.ToReadOnly();
            }, token).ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> CreatePathfindingHistoriesAsync(IEnumerable<PathfindingHistorySerializationModel> request,
            CancellationToken token = default)
        {
            var requests = mapper.Map<List<CreatePathfindingHistoryRequest<T>>>(request);
            return await CreatePathfindingHistoriesAsync(requests, token);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<T> request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var verticesIds = request.Select(x => x.Id);
                return await unitOfWork.RangeRepository
                    .DeleteByVerticesIdsAsync(verticesIds, t);
            }, token).ConfigureAwait(false);
        }

        public async Task<bool> DeleteRangeAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.RangeRepository.DeleteByGraphIdAsync(graphId, t), token).ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<GraphInformationModel>> ReadAllGraphInfoAsync(CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var result = (await unitOfWork.GraphRepository.GetAll(t)).ToList();
                var ids = result.Select(x => x.Id).ToList();
                var obstaclesCount = await unitOfWork.GraphRepository.ReadObstaclesCountAsync(ids, token)
                    .ConfigureAwait(false);
                var infos = mapper.Map<GraphInformationModel[]>(result).ToReadOnly();
                foreach (var info in infos)
                {
                    var runs = await unitOfWork.StatisticsRepository.ReadStatisticsCountAsync(info.Id, token)
                        .ConfigureAwait(false);
                    info.IsReadOnly = runs > 0;
                }
                infos.ForEach(x => x.ObstaclesCount = obstaclesCount[x.Id]);
                return infos;
            }, token).ConfigureAwait(false);
        }

        public async Task<GraphModel<T>> ReadGraphAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                return await unitOfWork.ReadGraphAsync<T>(graphId, mapper, t)
                    .ConfigureAwait(false);
            }, token).ConfigureAwait(false);
        }

        public async Task<GraphModel<T>> CreateGraphAsync(CreateGraphRequest<T> graph,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.AddGraphAsync<T>(mapper, graph, t).ConfigureAwait(false), token)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> ReadPathfindingHistoriesAsync(IEnumerable<int> graphIds,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var result = new List<PathfindingHistoryModel<T>>();
                await foreach (var graphId in graphIds.ToAsyncEnumerable()
                    .WithCancellation(token).ConfigureAwait(false))
                {
                    var graph = await unitOfWork.ReadGraphAsync<T>(graphId, mapper, t).ConfigureAwait(false);
                    var statistics = await unitOfWork.StatisticsRepository.ReadByGraphIdAsync(graphId, token).ConfigureAwait(false);
                    var range = await unitOfWork.GetRangeAsync(graphId, mapper, t).ConfigureAwait(false);
                    result.Add(new()
                    {
                        Graph = graph,
                        Statistics = mapper.Map<List<RunStatisticsModel>>(statistics),
                        Range = range.Select(x => x.Position).ToReadOnly()
                    });
                }
                return result;
            }, token).ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<PathfindingRangeModel>> ReadRangeAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.GetRangeAsync(graphId, mapper, t).ConfigureAwait(false), token)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<RunStatisticsModel>> ReadStatisticsAsync(int graphId,
            CancellationToken token = default)
        {
            return await Transaction(async (unit, t) =>
            {
                var result = await unit.StatisticsRepository.ReadByGraphIdAsync(graphId, token).ConfigureAwait(false);
                return mapper.Map<List<RunStatisticsModel>>(result);
            }, token);
        }

        public async Task<IReadOnlyCollection<PathfindingHistorySerializationModel>> ReadSerializationHistoriesAsync(IEnumerable<int> graphIds,
            CancellationToken token = default)
        {
            var result = await ReadPathfindingHistoriesAsync(graphIds, token);
            return await mapper.MapAsync<List<PathfindingHistorySerializationModel>>(result, token);
        }

        public async Task<bool> UpdateVerticesAsync(UpdateVerticesRequest<T> request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var repo = unitOfWork.VerticesRepository;
                return await (await mapper.MapAsync<Vertex[]>(request.Vertices, token))
                       .ForEach(x => x.GraphId = request.GraphId)
                       .ToAsync(async (x, tkn) => await repo.UpdateVerticesAsync(x, tkn), t);
            }, token).ConfigureAwait(false);
        }

        public async Task<bool> DeleteGraphsAsync(IEnumerable<int> ids,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.GraphRepository.DeleteAsync(ids, t).ConfigureAwait(false), token)
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteRunsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            return await Transaction(async (unit, t) =>
            {
                return await unit.StatisticsRepository.DeleteByIdsAsync(runIds, t).ConfigureAwait(false);
            }, token).ConfigureAwait(false);
        }

        private IReadOnlyCollection<PathfindingRange> SelectRangeEntities(IEnumerable<(int Order, T Vertex)> vertices, int graphId)
        {
            var items = vertices.ToList();
            return items.Select((x, i) => new PathfindingRange
            {
                GraphId = graphId,
                VertexId = x.Vertex.Id,
                Order = x.Order,
                IsSource = x.Order == 0,
                IsTarget = x.Order == items.Count - 1 && items.Count > 1
            }).ToReadOnly();
        }

        private async Task<TParam> Transaction<TParam>(
            Func<IUnitOfWork, CancellationToken, Task<TParam>> action,
            CancellationToken token)
        {
            using var unitOfWork = factory();
            try
            {
                unitOfWork.BeginTransaction();
                var result = await action(unitOfWork, token).ConfigureAwait(false);
                await unitOfWork.CommitAsync(token).ConfigureAwait(false);
                return result;
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync(token).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> CreatePathfindingVertexAsync(int graphId,
            int vertexId, int index, CancellationToken token = default)
        {
            return await Transaction(async (unit, t) =>
            {
                var range = (await unit.RangeRepository.ReadByGraphIdAsync(graphId, t)).ToList();
                var pathfindingRange = new PathfindingRange() { GraphId = graphId, Order = index, VertexId = vertexId };
                range.Insert(index, pathfindingRange);
                range.ForEach((x, i) =>
                {
                    x.IsSource = i == 0;
                    x.IsTarget = i == range.Count - 1;
                    x.Order = i;
                });
                await unit.RangeRepository.UpsertAsync(range, t);
                return true;
            }, token);
        }

        public async Task<RunStatisticsModel> CreateStatisticsAsync(CreateStatisticsRequest request, CancellationToken token = default)
        {
            return await Transaction(async (unit, t) =>
            {
                var statistics = mapper.Map<Statistics>(request);
                var result = await unit.StatisticsRepository.CreateAsync(statistics, t);
                return mapper.Map<RunStatisticsModel>(result);
            }, token).ConfigureAwait(false);
        }

        public async Task<RunStatisticsModel> ReadStatisticAsync(int runId, CancellationToken token = default)
        {
            return await Transaction(async (unit, t) =>
            {
                var statistic = await unit.StatisticsRepository.ReadByIdAsync(runId, token);
                return mapper.Map<RunStatisticsModel>(statistic);
            }, token);
        }

        public async Task<GraphInformationModel> ReadGraphInfoAsync(int graphId, CancellationToken token = default)
        {
            using var unitOfWork = factory.Invoke();
            var result = await unitOfWork.GraphRepository.ReadAsync(graphId, token)
                .ConfigureAwait(false);
            return mapper.Map<GraphInformationModel>(result);
        }

        public async Task<bool> UpdateGraphInfoAsync(GraphInformationModel graph, CancellationToken token = default)
        {
            return await Transaction(async (unit, t) =>
            {
                var graphInfo = mapper.Map<Graph>(graph);
                return await unit.GraphRepository.UpdateAsync(graphInfo, t);
            }, token);
        }
    }
}
