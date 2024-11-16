using Pathfinding.Domain.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Domain.Interface.Repositories
{
    public interface IVerticesRepository
    {
        Task<IEnumerable<Vertex>> ReadVerticesByGraphIdAsync(int graphId,
            CancellationToken token = default);

        Task<IEnumerable<Vertex>> CreateAsync(IEnumerable<Vertex> vertices,
            CancellationToken token = default);

        Task<bool> DeleteVerticesByGraphIdAsync(int graphId,
            CancellationToken token = default);

        Task<bool> UpdateVerticesAsync(IEnumerable<Vertex> vertices,
            CancellationToken token = default);

        Task<Vertex> ReadAsync(long vertexId, CancellationToken token = default);
    }
}
