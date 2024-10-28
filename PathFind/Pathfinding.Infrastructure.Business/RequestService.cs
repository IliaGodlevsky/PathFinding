using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.InMemory;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
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

        public async Task<IReadOnlyCollection<AlgorithmRunHistoryModel>> CreateRunHistoriesAsync(IEnumerable<CreateAlgorithmRunHistoryRequest> histories,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var result = await unitOfWork.AddHistoryAsync(mapper, histories);
                return mapper.Map<AlgorithmRunHistoryModel[]>(result).ToReadOnly();
            }, token).ConfigureAwait(false);
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
                var obstaclesCount = await unitOfWork.GraphRepository.ReadObstaclesCountAsync(ids, token);
                var infos = mapper.Map<GraphInformationModel[]>(result).ToReadOnly();
                foreach (var info in infos)
                {
                    var runs = await unitOfWork.RunRepository.ReadByGraphIdAsync(info.Id, token);
                    info.IsReadOnly = runs.Any();
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
                var result = await unitOfWork.ReadGraphAsync<T>(graphId, mapper, t);
                var factory = result.Neighborhood == NeighborhoodNames.Moore 
                    ? (INeighborhoodFactory)new MooreNeighborhoodFactory() 
                    : new VonNeumannNeighborhoodFactory();
                var layer = new NeighborhoodLayer(factory);
                layer.Overlay((IGraph<IVertex>)result.Graph);
                return result;
            }, token).ConfigureAwait(false);
        }

        public async Task<GraphModel<T>> CreateGraphAsync(GraphSerializationModel graph,
            CancellationToken token = default)
        {
            var mapped = mapper.Map<CreateGraphRequest<T>>(graph);
            return await CreateGraphAsync(mapped, token);
        }

        public async Task<GraphModel<T>> CreateGraphAsync(CreateGraphRequest<T> graph,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.AddGraphAsync<T>(mapper, graph, t), token).ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> ReadPathfindingHistoriesAsync(IEnumerable<int> graphIds,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var result = new List<PathfindingHistoryModel<T>>();
                foreach (var graphId in graphIds)
                {
                    var graph = await unitOfWork.ReadGraphAsync<T>(graphId, mapper, t);
                    var algorithms = await unitOfWork.GetAlgorithmRuns(graphId, mapper, t);
                    var range = await unitOfWork.GetRangeAsync(graphId, mapper, t);
                    result.Add(new()
                    {
                        Graph = graph,
                        Algorithms = algorithms,
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
                => await unitOfWork.GetRangeAsync(graphId, mapper, t), token).ConfigureAwait(false);
        }

        public async Task<AlgorithmRunHistoryModel> ReadRunHistoryAsync(int runId,
            CancellationToken token = default)
        {
            return await Transaction(async (unit, t) =>
            {
                var statistics = await unit.StatisticsRepository.ReadByAlgorithmRunIdAsync(runId, t);
                var run = await unit.RunRepository.ReadAsync(runId, t);
                var info = await unit.GraphRepository.ReadAsync(run.GraphId, t);
                return new AlgorithmRunHistoryModel()
                {
                    GraphInfo = mapper.Map<GraphInformationModel>(info),
                    Statistics = mapper.Map<RunStatisticsModel>(statistics) with { AlgorithmId = run.AlgorithmId },
                    Run = mapper.Map<AlgorithmRunModel>(run)
                };
            }, token).ConfigureAwait(false);
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
                    .ToReadOnly();
            }, token);
        }

        public async Task<IReadOnlyCollection<PathfindingHistorySerializationModel>> ReadSerializationHistoriesAsync(IEnumerable<int> graphIds,
            CancellationToken token = default)
        {
            var result = await ReadPathfindingHistoriesAsync(graphIds, token);
            return mapper.Map<List<PathfindingHistorySerializationModel>>(result);
        }

        //public async Task<bool> UpdateObstaclesCountAsync(UpdateGraphInfoRequest request,
        //    CancellationToken token = default)
        //{
        //    return await Transaction(async (unitOfWork, t) =>
        //    {
        //        var entity = await unitOfWork.GraphRepository.ReadAsync(request.Id, t);
        //        entity.ObstaclesCount = request.ObstaclesCount;
        //        return await unitOfWork.GraphRepository.UpdateAsync(entity, t);
        //    }, token).ConfigureAwait(false);
        //}

        public async Task<bool> UpdateVerticesAsync(UpdateVerticesRequest<T> request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var repo = unitOfWork.VerticesRepository;
                return await mapper.Map<Vertex[]>(request.Vertices)
                       .ForEach(x => x.GraphId = request.GraphId)
                       .ToAsync(async (x, tkn) => await repo.UpdateVerticesAsync(x, tkn), t);
            }, token).ConfigureAwait(false);
        }

        public async Task<bool> DeleteGraphsAsync(IEnumerable<int> ids,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t)
                => await unitOfWork.GraphRepository.DeleteAsync(ids, t), token).ConfigureAwait(false);
        }

        public async Task<bool> UpsertRangeAsync(UpsertPathfindingRangeRequest request,
            CancellationToken token = default)
        {
            return await Transaction(async (unitOfWork, t) =>
            {
                var range = request.Ranges.Select(x => new PathfindingRange()
                {
                    Id = x.Id,
                    IsSource = x.IsSource,
                    IsTarget = x.IsTarget,
                    VertexId = x.VertexId,
                    GraphId = request.GraphId,
                    Order = x.Order
                }).ToList();
                await unitOfWork.RangeRepository.UpsertAsync(range, t);
                return true;
            }, token).ConfigureAwait(false);
        }

        public async Task<bool> DeleteRunsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            return await Transaction(async (unit, t) =>
            {
                return await unit.RunRepository.DeleteByRunIdsAsync(runIds, t);
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
    }
}
