using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    public sealed class LiteDbNeighborsRepository : INeighborsRepository
    {
        private readonly ILiteCollection<Neighbor> collection;
        private readonly ILiteCollection<Vertex> vertices;

        public LiteDbNeighborsRepository(ILiteDatabase db)
        {
            vertices = db.GetCollection<Vertex>(DbTables.Vertices);
            collection = db.GetCollection<Neighbor>(DbTables.Neighbors);
            collection.EnsureIndex(x => x.VertexId);
        }

        public async Task<IEnumerable<Neighbor>> CreateAsync(IEnumerable<Neighbor> neighbours, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                collection.Insert(neighbours);
                return neighbours;
            }, token);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                var bsonIds = vertices
                    .Find(x => x.GraphId == graphId)
                    .Select(x => new BsonValue(x.Id))
                    .ToArray();
                var query = Query.In(nameof(Neighbor.VertexId), bsonIds);
                int deleted = collection.DeleteMany(query);
                return deleted > 0;
            }, token);
        }

        public async Task<bool> DeleteNeighbourAsync(int vertexId, int neighbourId, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                int deleted = collection.DeleteMany(x => x.VertexId == vertexId
                          && x.NeighborId == neighbourId);
                return deleted == 1;
            }, token);

        }

        public async Task<bool> DeleteNeighboursAsync(IEnumerable<(int VertexId, List<int> NeighborsIds)> neighbors,
            CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                var query = neighbors.Select(x =>
                {
                    var inQuery = Query.In(nameof(Neighbor.NeighborId),
                        x.NeighborsIds.Select(y => new BsonValue(x)));
                    var equalQuery = Query.EQ(nameof(Neighbor.VertexId), x.VertexId);
                    return Query.And(inQuery, equalQuery);
                }).Aggregate((x, y) => Query.Or(x, y));
                return collection.DeleteMany(query) > 0;
            }, token);
        }

        public async Task<IReadOnlyDictionary<int, IReadOnlyCollection<Neighbor>>> ReadNeighboursForVerticesAsync(IEnumerable<int> verticesIds, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                var bsonIds = verticesIds.Select(x => new BsonValue(x)).ToArray();
                var query = Query.In(nameof(Neighbor.VertexId), bsonIds);
                return collection.Find(query)
                    .GroupBy(x => x.VertexId)
                    .ToDictionary(x => x.Key, x => (IReadOnlyCollection<Neighbor>)x.ToReadOnly())
                    .AsReadOnly();
            }, token);
        }
    }
}
