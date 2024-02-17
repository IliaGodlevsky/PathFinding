using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbNeighborsRepository : INeighborsRepository
    {
        private readonly ILiteCollection<NeighborEntity> collection;
        private readonly ILiteCollection<VertexEntity> vertices;

        public LiteDbNeighborsRepository(ILiteDatabase db)
        {
            vertices = db.GetCollection<VertexEntity>(DbTables.Vertices);
            collection = db.GetCollection<NeighborEntity>(DbTables.Neighbors);
            collection.EnsureIndex(x => x.VertexId);
        }

        public IEnumerable<NeighborEntity> Insert(IEnumerable<NeighborEntity> neighbours)
        {
            collection.Insert(neighbours);
            return neighbours;
        }

        public bool DeleteByGraphId(int graphId)
        {
            var bsonIds = vertices.Find(x => x.GraphId == graphId)
                .Select(x => new BsonValue(x.Id)).ToArray();
            var query = Query.In(nameof(NeighborEntity.VertexId), bsonIds);
            int deleted = collection.DeleteMany(query);
            return deleted > 0;
        }

        public bool DeleteNeighbour(int vertexId, int neighbourId)
        {
            int deleted = collection.DeleteMany(x => x.VertexId == vertexId
                          && x.NeighborId == neighbourId);
            return deleted == 1;
        }

        public IReadOnlyDictionary<int, IReadOnlyCollection<NeighborEntity>> GetNeighboursForVertices(IEnumerable<int> verticesIds)
        {
            var bsonIds = verticesIds.Select(x => new BsonValue(x)).ToArray();
            var query = Query.In(nameof(NeighborEntity.VertexId), bsonIds);
            return collection.Find(query)
                .GroupBy(x => x.VertexId)
                .ToDictionary(x => x.Key, x => (IReadOnlyCollection<NeighborEntity>)x.ToReadOnly())
                .AsReadOnly();
        }
    }
}
