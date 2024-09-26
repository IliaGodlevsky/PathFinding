using Pathfinding.Domain.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Domain.Interface.Repositories
{
    public interface IGraphParametresRepository
    {
        Task<int> ReadCountAsync(CancellationToken token = default);

        Task<Graph> ReadAsync(int graphId, CancellationToken token = default);

        Task<Graph> CreateAsync(Graph graph, CancellationToken token = default);

        Task<bool> DeleteAsync(int graphId, CancellationToken token = default);

        Task<bool> DeleteAsync(IEnumerable<int> graphIds, CancellationToken token = default);

        Task<bool> UpdateAsync(Graph graph, CancellationToken token = default);

        Task<IEnumerable<Graph>> GetAll(CancellationToken token = default);
    }
}
