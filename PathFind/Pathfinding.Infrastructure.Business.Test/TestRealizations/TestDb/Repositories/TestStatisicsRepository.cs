using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb.Repositories
{
    internal sealed class TestStatisicsRepository : IStatisticsRepository
    {
        public async Task<Statistics> CreateAsync(Statistics entity,
            CancellationToken token = default)
        {
            entity.Id = 1;
            return await Task.FromResult(entity);
        }

        public async Task<Statistics> ReadByAlgorithmRunIdAsync(int runId,
            CancellationToken token = default)
        {
            var statistics = new Statistics()
            {
                Id = runId,
                AlgorithmRunId = 1,
                Cost = 1,
                Elapsed = TimeSpan.FromMilliseconds(1),
                Heuristics = string.Empty,
                StepRule = string.Empty,
                Spread = 1,
                Steps = 1,
                ResultStatus = "TEST",
                Visited = 1,
            };
            return await Task.FromResult(statistics);
        }

        public async Task<IEnumerable<Statistics>> ReadByRunIdsAsync(IEnumerable<int> runIds,
            CancellationToken token = default)
        {
            var statistics = new Statistics()
            {
                Id = 1,
                AlgorithmRunId = 1,
                Cost = 1,
                Elapsed = TimeSpan.FromMilliseconds(1),
                Heuristics = string.Empty,
                StepRule = string.Empty,
                Spread = 1,
                Steps = 1,
                ResultStatus = "TEST",
                Visited = 1,
            };
            return await Task.FromResult(statistics.Enumerate());
        }
    }
}
