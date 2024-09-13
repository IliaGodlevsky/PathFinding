using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.InMemory.Repositories
{
    internal sealed class InMemoryGraphStateRepository : IGraphStateRepository
    {
        private int id = 0;

        private readonly HashSet<GraphState> set = new(EntityComparer<int>.Instance);

        public async Task<GraphState> CreateAsync(GraphState entity,
            CancellationToken token = default)
        {
            entity.Id = ++id;
            set.Add(entity);
            return await Task.FromResult(entity);
        }

        public async Task<GraphState> ReadByRunIdAsync(int runId,
            CancellationToken token = default)
        {
            return await Task.FromResult(set.FirstOrDefault(x => x.AlgorithmRunId == runId));
        }

        public async Task<bool> DeleteByRunIdsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            var ids = runIds.ToHashSet();
            int deleted = set.RemoveWhere(x => ids.Contains(x.AlgorithmRunId));
            return await Task.FromResult(ids.Count == deleted);
        }
    }
}
