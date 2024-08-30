using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    internal sealed class LiteDbRangeRepository : IRangeRepository
    {
        private readonly ILiteCollection<PathfindingRange> collection;

        public LiteDbRangeRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<PathfindingRange>(DbTables.Ranges);
        }

        public async Task<IEnumerable<PathfindingRange>> CreateAsync(IEnumerable<PathfindingRange> entities, CancellationToken token = default)
        {
            collection.Insert(entities);
            return await Task.FromResult(entities);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            int deleted = collection.DeleteMany(x => x.GraphId == graphId);
            return await Task.FromResult(deleted > 0);
        }

        public async Task<bool> DeleteByVertexIdAsync(int vertexId, CancellationToken token = default)
        {
            int deleted = collection.DeleteMany(x => x.VertexId == vertexId);
            return await Task.FromResult(deleted > 0);
        }

        public async Task<bool> DeleteByVerticesIdsAsync(IEnumerable<int> verticesIds, CancellationToken token = default)
        {
            var ids = verticesIds.Select(x => new BsonValue(x)).ToArray();
            var query = Query.In(nameof(PathfindingRange.VertexId), ids);
            return await Task.FromResult(collection.DeleteMany(query) > 0);
        }

        public async Task<IEnumerable<PathfindingRange>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            await Task.CompletedTask;
            return collection.Query()
                .Where(x => x.GraphId == graphId)
                .OrderBy(x => x.Order)
                .ToEnumerable();
        }

        public async Task<IEnumerable<PathfindingRange>> ReadByVerticesIdsAsync(IEnumerable<int> verticesIds, CancellationToken token = default)
        {
            var ids = verticesIds.Select(x => new BsonValue(x)).ToArray();
            var query = Query.In(nameof(PathfindingRange.VertexId), ids);
            return await Task.FromResult(collection.Find(query).OrderBy(x => x.Order));
        }

        public async Task<bool> UpdateAsync(IEnumerable<PathfindingRange> entities, CancellationToken token = default)
        {
            collection.Update(entities);
            return await Task.FromResult(true);
        }
    }
}
