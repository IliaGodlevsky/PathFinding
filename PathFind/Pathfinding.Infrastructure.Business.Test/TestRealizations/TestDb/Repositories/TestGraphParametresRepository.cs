using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb.Repositories
{
    internal sealed class TestGraphParametresRepository : IGraphParametresRepository
    {
        public async Task<Graph> CreateAsync(Graph graph,
            CancellationToken token = default)
        {
            graph.Id = 1;
            return await Task.FromResult(graph);
        }

        public async Task<bool> DeleteAsync(int graphId,
            CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(IEnumerable<int> graphIds,
            CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }

        public Task<IEnumerable<Graph>> GetAll(CancellationToken token = default)
        {
            return Task.FromResult(Enumerable.Empty<Graph>());
        }

        public async Task<Graph> ReadAsync(int graphId,
            CancellationToken token = default)
        {
            var result = new Graph()
            {
                Dimensions = "[25,35]",
                Id = graphId,
                Name = "TEST",
                SmoothLevel = "TEST",
                Neighborhood = "TEST"
            };
            return await Task.FromResult(result);
        }

        public async Task<IReadOnlyDictionary<int, int>> ReadObstaclesCountAsync(IEnumerable<int> graphIds, CancellationToken token = default)
        {
            return await Task.FromResult(graphIds.ToDictionary(x => x, x => 1));
        }

        public async Task<bool> UpdateAsync(Graph graph,
            CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }
    }
}
