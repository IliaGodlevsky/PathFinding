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
    }
}
