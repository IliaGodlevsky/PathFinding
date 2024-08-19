using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Test.Mock.TestUnitOfWork
{
    internal sealed class TestSubAlgorithmRepository : ISubAlgorithmRepository
    {
        private int id = 0;

        private readonly HashSet<SubAlgorithm> set = new(EntityComparer<int>.Instance);

        public async Task<IEnumerable<SubAlgorithm>> CreateAsync(IEnumerable<SubAlgorithm> entities, CancellationToken token = default)
        {
            var result = entities
                .ForEach(x => x.Id = ++id)
                .ForWhole(set.AddRange)
                .ToList();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<SubAlgorithm>> ReadByAlgorithmRunIdAsync(int runId, CancellationToken token = default)
        {
            var result = set.Where(x => x.AlgorithmRunId == runId).ToList();
            return await Task.FromResult(result);
        }
    }
}
