using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
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
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business
{
    public sealed class CachedRequestService<T> : IRequestService<T>
        where T : IVertex, IEntity<int>
    {
        private static readonly TimeSpan CacheLifeTime = TimeSpan.FromHours(2);

        private readonly IRequestService<T> service;
        private readonly MemoryCache graphs = new("Graphs");

        public CachedRequestService(IRequestService<T> service)
        {
            this.service = service;
        }

        public async Task<GraphModel<T>> CreateGraphAsync(GraphSerializationModel graph, CancellationToken token = default)
        {
            var model = await service.CreateGraphAsync(graph, token);
            graphs.Set(model.Id.ToString(), model, DateTime.UtcNow.Add(CacheLifeTime));
            return model;
        }

        public async Task<GraphModel<T>> CreateGraphAsync(CreateGraphRequest<T> graph, CancellationToken token = default)
        {
            var model = await service.CreateGraphAsync(graph, token);
            graphs.Set(model.Id.ToString(), model, DateTime.UtcNow.Add(CacheLifeTime));
            return model;
        }

        public async Task<IReadOnlyCollection<GraphModel<T>>> CreateGraphsAsync(IEnumerable<GraphSerializationModel> request, CancellationToken token = default)
        {
            var models = await service.CreateGraphsAsync(request, token);
            foreach(var model in models.ToArray())
            {
                graphs.Set(model.Id.ToString(), model, DateTime.UtcNow.Add(CacheLifeTime));
            }
            return models;
        }

        public async Task<bool> CreateNeighborsAsync(CreateNeighborsRequest<T> request, CancellationToken token = default)
        {
            return await service.CreateNeighborsAsync(request, token);
        }

        public async Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> CreatePathfindingHistoriesAsync(IEnumerable<CreatePathfindingHistoryRequest<T>> request, CancellationToken token = default)
        {
            return await service.CreatePathfindingHistoriesAsync(request, token);
        }

        public async Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> CreatePathfindingHistoriesAsync(IEnumerable<PathfindingHistorySerializationModel> request, CancellationToken token = default)
        {
            return await service.CreatePathfindingHistoriesAsync(request, token);
        }

        public async Task<bool> CreateRangeAsync(CreatePathfindingRangeRequest<T> request, CancellationToken token = default)
        {
            return await service.CreateRangeAsync(request, token);
        }

        public async Task<IReadOnlyCollection<AlgorithmRunHistoryModel>> CreateRunHistoriesAsync(IEnumerable<CreateAlgorithmRunHistoryRequest> histories, CancellationToken token = default)
        {
            return await service.CreateRunHistoriesAsync(histories, token);
        }

        public async Task<bool> DeleteGraphAsync(int graphId, CancellationToken token = default)
        {
            if (await service.DeleteGraphAsync(graphId, token))
            {
                graphs.Remove(graphId.ToString());
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteGraphsAsync(IEnumerable<int> ids, CancellationToken token = default)
        {
            if (await service.DeleteGraphsAsync(ids, token))
            {
                foreach (int id in ids.ToArray())
                {
                    graphs.Remove(id.ToString());
                }
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<T> request, CancellationToken token = default)
        {
            return await service.DeleteRangeAsync(request, token);
        }

        public async Task<bool> DeleteRangeAsync(int graphId, CancellationToken token = default)
        {
            return await service.DeleteRangeAsync(graphId, token);
        }

        public async Task<bool> DeleteRunsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            return await service.DeleteRunsAsync(runIds, token);
        }

        public async Task<IReadOnlyCollection<GraphInformationModel>> ReadAllGraphInfoAsync(CancellationToken token = default)
        {
            return await service.ReadAllGraphInfoAsync(token);
        }

        public async Task<GraphModel<T>> ReadGraphAsync(int graphId, CancellationToken token = default)
        {
            var graph = graphs.Get(graphId.ToString()) as GraphModel<T>;
            if (graph == null)
            {
                graph = await service.ReadGraphAsync(graphId, token);
                graphs.Set(graph.Id.ToString(), graph, DateTime.UtcNow.Add(CacheLifeTime));
            }
            return graph;
        }

        public async Task<int> ReadGraphCountAsync(CancellationToken token = default)
        {
            return await service.ReadGraphCountAsync(token);
        }

        public async Task<IReadOnlyCollection<int>> ReadGraphIdsAsync(CancellationToken token = default)
        {
            return await service.ReadGraphIdsAsync(token);
        }

        public async Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> ReadPathfindingHistoriesAsync(IEnumerable<int> graphIds, CancellationToken token = default)
        {
            return await service.ReadPathfindingHistoriesAsync(graphIds, token);
        }

        public async Task<IReadOnlyCollection<PathfindingRangeModel>> ReadRangeAsync(int graphId, CancellationToken token = default)
        {
            return await service.ReadRangeAsync(graphId, token);
        }

        public async Task<int> ReadRunCountAsync(int graphId, CancellationToken token = default)
        {
            return await service.ReadRunCountAsync(graphId, token);
        }

        public async Task<RunVisualizationModel> ReadRunInfoAsync(int runId, CancellationToken token = default)
        {
            return await service.ReadRunInfoAsync(runId, token);
        }

        public async Task<IReadOnlyCollection<RunStatisticsModel>> ReadRunStatisticsAsync(int graphId, CancellationToken token = default)
        {
            return await service.ReadRunStatisticsAsync(graphId, token);
        }

        public async Task<GraphSerializationModel> ReadSerializationGraphAsync(int graphId, CancellationToken token = default)
        {
            return await service.ReadSerializationGraphAsync(graphId, token);
        }

        public async Task<IReadOnlyCollection<PathfindingHistorySerializationModel>> ReadSerializationHistoriesAsync(IEnumerable<int> graphIds, CancellationToken token = default)
        {
            return await service.ReadSerializationHistoriesAsync(graphIds, token);
        }

        public async Task<bool> RemoveNeighborsAsync(IReadOnlyDictionary<T, IReadOnlyCollection<T>> request, CancellationToken token = default)
        {
            return await service.RemoveNeighborsAsync(request, token);
        }

        public async Task<bool> UpdateObstaclesCountAsync(UpdateGraphInfoRequest request, CancellationToken token = default)
        {
            return await service.UpdateObstaclesCountAsync(request, token);
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<(int Order, T Vertex)> request, CancellationToken token = default)
        {
            return await service.UpdateRangeAsync(request, token);
        }

        public async Task<bool> UpdateVerticesAsync(UpdateVerticesRequest<T> request, CancellationToken token = default)
        {
            return await service.UpdateVerticesAsync(request, token);
        }

        public async Task<bool> UpsertRangeAsync(UpsertPathfindingRangeRequest request, CancellationToken token = default)
        {
            return await service.UpsertRangeAsync(request, token);
        }
    }
}
