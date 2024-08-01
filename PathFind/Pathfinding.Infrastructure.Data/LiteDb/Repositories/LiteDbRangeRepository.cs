using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    public sealed class LiteDbRangeRepository : IRangeRepository
    {
        private readonly ILiteCollection<PathfindingRange> collection;

        public LiteDbRangeRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<PathfindingRange>(DbTables.Ranges);
        }

        public async Task<IEnumerable<PathfindingRange>> CreateAsync(IEnumerable<PathfindingRange> entities, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                collection.Insert(entities);
                return entities;
            }, token);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                int deleted = collection.DeleteMany(x => x.GraphId == graphId);
                return deleted > 0;
            }, token);
        }

        public async Task<bool> DeleteByVertexIdAsync(int vertexId, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                int deleted = collection.DeleteMany(x => x.VertexId == vertexId);
                return deleted > 0;
            }, token);
        }

        public async Task<bool> DeleteByVerticesIdsAsync(IEnumerable<int> verticesIds, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                var ids = verticesIds.Select(x => new BsonValue(x)).ToArray();
                var query = Query.In(nameof(PathfindingRange.VertexId), ids);
                return collection.DeleteMany(query) > 0;
            }, token);
        }

        public async Task<IEnumerable<PathfindingRange>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                return collection.Query()
                .Where(x => x.GraphId == graphId)
                .OrderBy(x => x.Order)
                .ToEnumerable();
            }, token);
        }

        public async Task<IEnumerable<PathfindingRange>> ReadByVerticesIdsAsync(IEnumerable<int> verticesIds, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                var ids = verticesIds.Select(x => new BsonValue(x)).ToArray();
                var query = Query.In(nameof(PathfindingRange.VertexId), ids);
                return collection.Find(query).OrderBy(x => x.Order);
            }, token);

        }

        public async Task<bool> UpdateAsync(IEnumerable<PathfindingRange> entities, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                collection.Update(entities);
                return true;
            }, token);
        }
    }
}
