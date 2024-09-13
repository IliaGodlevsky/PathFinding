using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.InMemory.Repositories
{
    internal sealed class InMemoryAlgorithmRunRepository : IAlgorithmRunRepository
    {
        private int id = 0;

        private readonly HashSet<AlgorithmRun> set = new(EntityComparer<int>.Interface);

        private readonly InMemoryStatisicsRepository statistics;
        private readonly InMemoryGraphStateRepository graphStates;
        private readonly InMemorySubAlgorithmRepository subAlgorithms;

        public InMemoryAlgorithmRunRepository(InMemoryStatisicsRepository statistics, 
            InMemoryGraphStateRepository graphStates,
            InMemorySubAlgorithmRepository subAlgorithms)
        {
            this.statistics = statistics;
            this.graphStates = graphStates;
            this.subAlgorithms = subAlgorithms;
        }

        public async Task<AlgorithmRun> CreateAsync(AlgorithmRun entity,
            CancellationToken token = default)
        {
            entity.Id = ++id;
            set.Add(entity);
            return await Task.FromResult(entity);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId,
            CancellationToken token = default)
        {
            var runIds = set.Where(x => x.GraphId == graphId).Select(x => x.Id).ToHashSet();
            await statistics.DeleteByRunIdsAsync(runIds, token);
            await graphStates.DeleteByRunIdsAsync(runIds, token);
            await subAlgorithms.DeleteByRunIdsAsync(runIds, token);
            var deleted = set.RemoveWhere(x => x.GraphId == graphId);
            return await Task.FromResult(deleted > 0);
        }

        public async Task<IEnumerable<AlgorithmRun>> ReadByGraphIdAsync(int graphId,
            CancellationToken token = default)
        {
            var result = set.Where(x => x.GraphId == graphId).ToArray();
            return await Task.FromResult(result);
        }

        public async Task<int> ReadCount(int graphId, CancellationToken token = default)
        {
            int result = set.Count(x => x.GraphId == graphId);
            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteByRunIdsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            var ids = runIds.ToArray();
            var toDelete = set.Where(x => ids.Contains(x.Id)).Select(x => x.Id).ToHashSet();
            var deleted = set.RemoveWhere(x => toDelete.Contains(x.Id));
            await statistics.DeleteByRunIdsAsync(ids, token);
            await graphStates.DeleteByRunIdsAsync(ids, token);
            await subAlgorithms.DeleteByRunIdsAsync(ids, token);
            return await Task.FromResult(deleted > 0);
        }
    }
}
