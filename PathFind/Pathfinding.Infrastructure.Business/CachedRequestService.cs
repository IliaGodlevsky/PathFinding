using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Service.Interface.Requests.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business
{
    public sealed class CachedRequestService<T> : IRequestService<T>
        where T : IVertex, IEntity<long>
    {
        private static readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(30);

        private readonly MemoryCache graphs = new("Graphs");
        private readonly MemoryCache statistics = new("Statistics");
        private readonly MemoryCache ranges = new("Ranges");
        private readonly MemoryCache graphInfos = new("Graph infos");

        private readonly IRequestService<T> service;

        public CachedRequestService(IRequestService<T> service)
        {
            this.service = service;
        }

        public async Task<GraphModel<T>> CreateGraphAsync(CreateGraphRequest<T> graph, CancellationToken token = default)
        {
            var result = await service.CreateGraphAsync(graph, token);
            var policy = new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow + ExpirationTime
            };
            var item = new CacheItem(result.Id.ToString(), result.Graph);
            graphs.Add(item, policy);
            return result;
        }

        public async Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> CreatePathfindingHistoriesAsync(IEnumerable<CreatePathfindingHistoryRequest<T>> request, CancellationToken token = default)
        {
            var results = await service.CreatePathfindingHistoriesAsync(request, token)
                .ConfigureAwait(false);
            foreach (var graph in results.Select(x => x.Graph))
            {
                var policy = new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow + ExpirationTime
                };
                var item = new CacheItem(graph.Id.ToString(), graph.Graph);
                graphs.Add(item, policy);
            }
            return results;
        }

        public async Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> CreatePathfindingHistoriesAsync(IEnumerable<PathfindingHistorySerializationModel> request, CancellationToken token = default)
        {
            var results = await service.CreatePathfindingHistoriesAsync(request, token)
                .ConfigureAwait(false);
            foreach (var graph in results.Select(x => x.Graph))
            {
                var policy = new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow + ExpirationTime
                };
                var item = new CacheItem(graph.Id.ToString(), graph.Graph);
                graphs.Add(item, policy);
            }
            return results;
        }

        public async Task<bool> CreatePathfindingVertexAsync(int graphId, long vertexId, int index, CancellationToken token = default)
        {
            return await service.CreatePathfindingVertexAsync(graphId, vertexId, index, token)
                .ConfigureAwait(false);
        }

        public async Task<RunStatisticsModel> CreateStatisticsAsync(CreateStatisticsRequest request, CancellationToken token = default)
        {
            var result = await service.CreateStatisticsAsync(request, token).ConfigureAwait(false);
            var policy = new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow + ExpirationTime
            };
            var item = new CacheItem(result.Id.ToString(), result);
            statistics.Add(item, policy);
            return result;
        }

        public async Task<bool> DeleteGraphsAsync(IEnumerable<int> ids, CancellationToken token = default)
        {
            var result = await service.DeleteGraphsAsync(ids, token).ConfigureAwait(false);
            foreach (var id in ids)
            {
                graphs.Remove(ids.ToString());
            }
            return result;
        }

        public Task<bool> DeleteRangeAsync(IEnumerable<T> request, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(int graphId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteRunsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            var result = await service.DeleteRunsAsync(runIds, token).ConfigureAwait(false);
            foreach (var id in runIds)
            {
                statistics.Remove(id.ToString());
            }
            return result;
        }

        public Task<IReadOnlyCollection<GraphInformationModel>> ReadAllGraphInfoAsync(CancellationToken token = default)
        {

        }

        public Task<GraphModel<T>> ReadGraphAsync(int graphId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<GraphInformationModel> ReadGraphInfoAsync(int graphId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> ReadPathfindingHistoriesAsync(IEnumerable<int> graphIds, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<PathfindingRangeModel>> ReadRangeAsync(int graphId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<PathfindingHistorySerializationModel>> ReadSerializationHistoriesAsync(IEnumerable<int> graphIds, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<RunStatisticsModel> ReadStatisticAsync(int runId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<RunStatisticsModel>> ReadStatisticsAsync(int graphId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<RunStatisticsModel>> ReadStatisticsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateGraphInfoAsync(GraphInformationModel graph, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStatisticsAsync(IEnumerable<RunStatisticsModel> models, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateVerticesAsync(UpdateVerticesRequest<T> request, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
