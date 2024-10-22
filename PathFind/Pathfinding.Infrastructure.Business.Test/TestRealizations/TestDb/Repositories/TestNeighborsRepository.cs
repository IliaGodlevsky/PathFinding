using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb.Repositories
{
    internal sealed class TestNeighborsRepository : INeighborsRepository
    {
        private int id = 0;

        public async Task<IEnumerable<Neighbor>> CreateAsync(IEnumerable<Neighbor> neighbours,
            CancellationToken token = default)
        {
            foreach (var neighbor in neighbours)
            {
                neighbor.Id = ++id;
            }
            return await Task.FromResult(neighbours);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId,
            CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }

        public async Task<IReadOnlyDictionary<int, IReadOnlyCollection<Neighbor>>> ReadNeighboursForVerticesAsync(IEnumerable<int> verticesIds,
            CancellationToken token = default)
        {
            var dictionary = new Dictionary<int, IReadOnlyCollection<Neighbor>>();
            dictionary.Add(1, new[]
            {
                new Neighbor()
                {
                    GraphId = 1,
                    Id = 1,
                    NeighborId = 2,
                    VertexId = 1
                }
            });
            return await Task.FromResult(dictionary);
        }
    }
}
