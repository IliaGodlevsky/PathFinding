#nullable disable
using Pathfinding;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestUnitOfWork
{
    internal sealed class TestStatisicsRepository : IStatisticsRepository
    {
        private int id = 0;

        private readonly HashSet<Statistics> set = new(EntityComparer<int>.Instance);

        public Task<Statistics> CreateAsync(Statistics entity, CancellationToken token = default)
        {
            entity.Id = ++id;
            set.Add(entity);
            return Task.FromResult(entity);
        }

        public async Task<Statistics> ReadByAlgorithmRunIdAsync(int runId, CancellationToken token = default)
        {
            var result = set.FirstOrDefault(x => x.AlgorithmRunId == runId);
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Statistics>> ReadByRunIdsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            var result = set.Where(x => runIds.Contains(x.AlgorithmRunId)).ToList();
            return await Task.FromResult(result);
        }
    }
}
