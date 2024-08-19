using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    public sealed class LiteDbNeighborsRepository : INeighborsRepository
    {
        private readonly ILiteCollection<Neighbor> collection;

        public LiteDbNeighborsRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<Neighbor>(DbTables.Neighbors);
            collection.EnsureIndex(x => x.VertexId);
        }

        public async Task<IEnumerable<Neighbor>> CreateAsync(IEnumerable<Neighbor> neighbours, CancellationToken token = default)
        {
            collection.Insert(neighbours);
            return await Task.FromResult(neighbours);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            int deleted = collection.DeleteMany(x => x.GraphId == graphId);
            return await Task.FromResult(deleted > 0);
        }

        public async Task<bool> DeleteNeighbourAsync(int vertexId, int neighbourId, CancellationToken token = default)
        {
            int deleted = collection.DeleteMany(x => x.VertexId == vertexId
                      && x.NeighborId == neighbourId);
            return await Task.FromResult(deleted == 1);
        }

        public async Task<bool> DeleteNeighboursAsync(IEnumerable<(int VertexId, List<int> NeighborsIds)> neighbors,
            CancellationToken token = default)
        {
            var query = neighbors.Select(x =>
            {
                var inQuery = Query.In(nameof(Neighbor.NeighborId),
                    x.NeighborsIds.Select(y => new BsonValue(x)));
                var equalQuery = Query.EQ(nameof(Neighbor.VertexId), x.VertexId);
                return Query.And(inQuery, equalQuery);
            }).Aggregate((x, y) => Query.Or(x, y));
            return await Task.FromResult(collection.DeleteMany(query) > 0);
        }

        public async Task<IReadOnlyDictionary<int, IReadOnlyCollection<Neighbor>>> ReadNeighboursForVerticesAsync(IEnumerable<int> verticesIds, CancellationToken token = default)
        {
            await Task.CompletedTask;
            var bsonIds = verticesIds.Select(x => new BsonValue(x)).ToArray();
            var query = Query.In(nameof(Neighbor.VertexId), bsonIds);
            return collection.Find(query)
                .GroupBy(x => x.VertexId)
                .ToDictionary(x => x.Key, x => (IReadOnlyCollection<Neighbor>)x.ToReadOnly())
                .AsReadOnly();
        }
    }
}
