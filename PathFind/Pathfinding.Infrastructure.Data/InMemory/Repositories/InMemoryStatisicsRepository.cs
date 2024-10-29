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

        public Task<Statistics> CreateAsync(Statistics entity, CancellationToken token = default)
        {
            entity.Id = ++id;
            set.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Statistics>> CreateAsync(IEnumerable<Statistics> statistics, CancellationToken token = default)
        {
            foreach (var entity in statistics)
            {
                entity.Id = ++id;
                set.Add(entity);
            }
            return Task.FromResult(statistics);
        }

        public Task<IEnumerable<Statistics>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            var results = set.Where(s => s.GraphId == graphId).ToList();
            return Task.FromResult((IEnumerable<Statistics>)results);
        }

        public Task<int> ReadStatisticsCountAsync(int graphId, CancellationToken token = default)
        {
            var count = set.Count(s => s.GraphId == graphId);
            return Task.FromResult(count);
        }

        public Task<bool> DeleteByGraphId(int graphId, CancellationToken token = default)
        {
            var removed = set.RemoveWhere(s => s.GraphId == graphId) > 0;
            return Task.FromResult(removed);
        }

        public Task<bool> DeleteByIdsAsync(IEnumerable<int> ids, CancellationToken token = default)
        {
            var removed = set.RemoveWhere(s => ids.Contains(s.Id)) > 0;
            return Task.FromResult(removed);
        }

        public async Task<Statistics> ReadByIdAsync(int id, CancellationToken token = default)
        {
            var tracking = new Statistics() { Id = id };
            set.TryGetValue(tracking, out var statistics);
            return await Task.FromResult(statistics);
        }
    }
}
