using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Test.Mock.TestUnitOfWork
{
    internal sealed class TestRangeRepository : IRangeRepository
    {
        private int id = 0;

        private readonly HashSet<PathfindingRange> set = new(EntityComparer<int>.Instance);
        
        public async Task<IEnumerable<PathfindingRange>> CreateAsync(IEnumerable<PathfindingRange> entities, CancellationToken token = default)
        {
            var result = entities
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

        public async Task<bool> DeleteByVertexIdAsync(int vertexId, CancellationToken token = default)
        {
            var result = set.RemoveWhere(x => x.VertexId == vertexId);
            return await Task.FromResult(result > 0);
        }

        public async Task<bool> DeleteByVerticesIdsAsync(IEnumerable<int> verticesIds, CancellationToken token = default)
        {
            var result = set.RemoveWhere(x => verticesIds.Contains(x.VertexId));
            return await Task.FromResult(result > 0);
        }

        public async Task<IEnumerable<PathfindingRange>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            var result = set.Where(x => x.GraphId == graphId).ToList();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<PathfindingRange>> ReadByVerticesIdsAsync(IEnumerable<int> verticesIds, CancellationToken token = default)
        {
            var result = set.Where(x => verticesIds.Contains(x.VertexId)).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateAsync(IEnumerable<PathfindingRange> entities, CancellationToken token = default)
        {
            foreach (var entity in entities)
            {
                if (set.TryGetValue(entity, out var value))
                {
                    set.Remove(value);
                    set.Add(entity);
                }
            }
            return await Task.FromResult(true);
        }
    }
}
