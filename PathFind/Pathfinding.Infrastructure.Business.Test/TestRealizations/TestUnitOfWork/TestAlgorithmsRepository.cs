using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestUnitOfWork
{
    internal sealed class TestAlgorithmsRepository : IAlgorithmsRepository
    {
        public async Task<IEnumerable<Algorithm>> GetAllAsync(CancellationToken token = default)
        {
            return await Task.FromResult(AlgorithmNames.All
                .Select(x => new Algorithm { Name = x }).ToList());
        }
    }
}
