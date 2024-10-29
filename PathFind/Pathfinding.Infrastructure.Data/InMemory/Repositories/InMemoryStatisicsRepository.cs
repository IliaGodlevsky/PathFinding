using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.InMemory.Repositories
{
    internal sealed class InMemoryStatisicsRepository : IStatisticsRepository
    {
        private int id = 0;

        private readonly HashSet<Statistics> set = new(EntityComparer<int>.Instance);

        public Task<Statistics> CreateAsync(Statistics entity,
            CancellationToken token = default)
        {
            entity.Id = ++id;
            set.Add(entity);
            return Task.FromResult(entity);
        }

        public async Task<Statistics> ReadByAlgorithmRunIdAsync(int runId,
            CancellationToken token = default)
        {
            var result = set.FirstOrDefault(x => x.Id == runId);
            return await Task.FromResult(result);
        }

        public Task<IEnumerable<Statistics>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ReadStatisticsCountAsync(int graphId, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Statistics>> CreateAsync(IEnumerable<Statistics> statistics, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteByGraphId(int graphId, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteByIdsAsync(IEnumerable<int> ids, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Statistics> ReadByIdAsync(int runId, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
