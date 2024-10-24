using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb.Repositories
{
    internal sealed class TestSubAlgorithmRepository : ISubAlgorithmRepository
    {
        private int id = 0;

        public async Task<IEnumerable<SubAlgorithm>> CreateAsync(IEnumerable<SubAlgorithm> entities,
            CancellationToken token = default)
        {
            var result = entities
                .ForEach(x => x.Id = ++id)
                .ToList();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<SubAlgorithm>> ReadByAlgorithmRunIdAsync(int runId,
            CancellationToken token = default)
        {
            var result = new SubAlgorithm()
            {
                Id = runId,
                AlgorithmRunId = 1,
                Order = 0,
                Path = Array.Empty<byte>(),
                Visited = Array.Empty<byte>()
            };
            return await Task.FromResult(result.Enumerate());
        }
    }
}
