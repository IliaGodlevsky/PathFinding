using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb.Repositories
{
    internal sealed class TestRangeRepository : IRangeRepository
    {
        private int id = 0;

        public async Task<IEnumerable<PathfindingRange>> CreateAsync(IEnumerable<PathfindingRange> entities,
            CancellationToken token = default)
        {
            var result = entities
                .ForEach(x => x.Id = ++id)
                .ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId,
            CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteByVertexIdAsync(int vertexId,
            CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteByVerticesIdsAsync(IEnumerable<int> verticesIds,
            CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<PathfindingRange>> ReadByGraphIdAsync(int graphId,
            CancellationToken token = default)
        {
            var range = new PathfindingRange()
            {
                Id = 1,
                GraphId = graphId,
                IsSource = false,
                IsTarget = false,
                Order = 1,
                VertexId = 1
            };
            return await Task.FromResult(range.Enumerate());
        }

        public async Task<IEnumerable<PathfindingRange>> ReadByVerticesIdsAsync(IEnumerable<int> verticesIds,
            CancellationToken token = default)
        {
            var range = new PathfindingRange()
            {
                Id = 1,
                GraphId = 1,
                IsSource = false,
                IsTarget = false,
                Order = 1,
                VertexId = 1
            };
            return await Task.FromResult(range.Enumerate());
        }

        public async Task<bool> UpdateAsync(IEnumerable<PathfindingRange> entities,
            CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<PathfindingRange>> UpsertAsync(IEnumerable<PathfindingRange> entities, CancellationToken token = default)
        {
            var range = new PathfindingRange()
            {
                Id = 1,
                GraphId = 1,
                IsSource = false,
                IsTarget = false,
                Order = 1,
                VertexId = 1
            };
            return await Task.FromResult(range.Enumerate());
        }
    }
}
