using Pathfinding.Domain.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Domain.Interface.Repositories
{
    public interface IGraphStateRepository
    {
        Task<GraphState> CreateAsync(GraphState entity, CancellationToken token = default);

        Task<GraphState> ReadByRunIdAsync(int runId, CancellationToken token = default);
    }
}
