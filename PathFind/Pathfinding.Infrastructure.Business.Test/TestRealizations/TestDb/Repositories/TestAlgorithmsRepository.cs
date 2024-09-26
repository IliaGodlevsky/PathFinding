using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb.Repositories
{
    internal sealed class TestAlgorithmsRepository : IAlgorithmsRepository
    {
        public async Task<IEnumerable<Algorithm>> GetAllAsync(CancellationToken token = default)
        {
            var result = AlgorithmNames.All
                .Select(x => new Algorithm { Name = x })
                .ToList();
            return await Task.FromResult(result);
        }
    }
}
