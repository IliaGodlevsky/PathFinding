using Pathfinding.Domain.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Domain.Interface.Repositories
{
    public interface ISubAlgorithmRepository
    {
        Task<IEnumerable<SubAlgorithm>> ReadByAlgorithmRunIdAsync(int runId,
            CancellationToken token = default);

        Task<IEnumerable<SubAlgorithm>> CreateAsync(IEnumerable<SubAlgorithm> entities,
            CancellationToken token = default);
    }
}
