using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.InMemory.Repositories
{
    internal sealed class InMemorySubAlgorithmRepository : ISubAlgorithmRepository
    {
        private int id = 0;

        private readonly HashSet<SubAlgorithm> set = new(EntityComparer<int>.Instance);

        public async Task<IEnumerable<SubAlgorithm>> CreateAsync(IEnumerable<SubAlgorithm> entities,
            CancellationToken token = default)
        {
            var result = entities
                .ForEach(x => x.Id = ++id)
                .ForWhole(set.AddRange)
                .ToList();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<SubAlgorithm>> ReadByAlgorithmRunIdAsync(int runId,
            CancellationToken token = default)
        {
            var result = set.Where(x => x.AlgorithmRunId == runId).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteByRunIdsAsync(IEnumerable<int> runIds,
            CancellationToken token = default)
        {
            var ids = runIds.ToHashSet();
            int deleted = set.RemoveWhere(x => ids.Contains(x.AlgorithmRunId));
            return await Task.FromResult(ids.Count == deleted);
        }
    }
}
