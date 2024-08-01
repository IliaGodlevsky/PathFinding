using Pathfinding.Domain.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Domain.Interface.Repositories
{
    public interface IStatisticsRepository
    {
        Task<Statistics> ReadByAlgorithmRunIdAsync(int runId, CancellationToken token = default);

        Task<IEnumerable<Statistics>> ReadByRunIdsAsync(IEnumerable<int> runIds,
            CancellationToken token = default);

        Task<Statistics> CreateAsync(Statistics entity, CancellationToken token = default);
    }
}
