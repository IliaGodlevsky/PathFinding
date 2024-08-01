using Pathfinding.Domain.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Domain.Interface.Repositories
{
    public interface IRangeRepository
    {
        Task<IEnumerable<PathfindingRange>> CreateAsync(IEnumerable<PathfindingRange> entities,
            CancellationToken token = default);

        Task<bool> DeleteByVertexIdAsync(int vertexId,
            CancellationToken token = default);

        Task<IEnumerable<PathfindingRange>> ReadByGraphIdAsync(int graphId,
            CancellationToken token = default);

        Task<IEnumerable<PathfindingRange>> ReadByVerticesIdsAsync(IEnumerable<int> verticesIds,
            CancellationToken token = default);

        Task<bool> DeleteByVerticesIdsAsync(IEnumerable<int> verticesIds,
            CancellationToken token = default);

        Task<bool> UpdateAsync(IEnumerable<PathfindingRange> entities,
            CancellationToken token = default);

        Task<bool> DeleteByGraphIdAsync(int graphId, CancellationToken token = default);
    }
}
