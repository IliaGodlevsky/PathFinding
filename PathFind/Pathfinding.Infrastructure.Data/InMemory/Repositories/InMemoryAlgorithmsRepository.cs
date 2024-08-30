using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.InMemory.Repositories
{
    internal sealed class InMemoryAlgorithmsRepository : IAlgorithmsRepository
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
