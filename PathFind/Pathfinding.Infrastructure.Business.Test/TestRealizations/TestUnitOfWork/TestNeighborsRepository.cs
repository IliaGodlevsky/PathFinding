using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestUnitOfWork
{
    internal sealed class TestNeighborsRepository : INeighborsRepository
    {
        private int id = 0;

        private readonly HashSet<Neighbor> set = new(EntityComparer<int>.Interface);

        public async Task<IEnumerable<Neighbor>> CreateAsync(IEnumerable<Neighbor> neighbours, CancellationToken token = default)
        {
            var result = neighbours
                .ForEach(x => x.Id = ++id)
                .ForWhole(set.AddRange)
                .ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            var result = set.RemoveWhere(x => x.GraphId == graphId);
            return await Task.FromResult(result > 0);
        }

        public async Task<bool> DeleteNeighbourAsync(int vertexId, int neighbourId, CancellationToken token = default)
        {
            var result = set
                .RemoveWhere(x => x.NeighborId == neighbourId && x.VertexId == vertexId);
            return await Task.FromResult(result == 1);
        }

        public async Task<bool> DeleteNeighboursAsync(IEnumerable<(int VertexId, List<int> NeighborsIds)> neighbors, CancellationToken token = default)
        {
            var n = neighbors.ToDictionary(x => x.VertexId, x => x.NeighborsIds);
            var result = set
                .RemoveWhere(x => n.ContainsKey(x.VertexId) && n[x.VertexId].Contains(x.NeighborId));
            return await Task.FromResult(result > 0);
        }

        public async Task<IReadOnlyDictionary<int, IReadOnlyCollection<Neighbor>>> ReadNeighboursForVerticesAsync(IEnumerable<int> verticesIds, CancellationToken token = default)
        {
            var result = set
                .Where(x => verticesIds.Contains(x.VertexId))
                .GroupBy(x => x.VertexId)
                .ToDictionary(x => x.Key, x => (IReadOnlyCollection<Neighbor>)x.ToReadOnly())
                .AsReadOnly();
            return await Task.FromResult(result);
        }
    }
}
