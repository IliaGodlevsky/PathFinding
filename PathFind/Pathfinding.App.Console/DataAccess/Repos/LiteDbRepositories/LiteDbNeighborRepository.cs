using LiteDB;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.Extensions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos.LiteDbRepositories
{
    internal sealed class LiteDbNeighborRepository : INeighborsRepository
    {
        private readonly ILiteCollection<NeighbourEntity> collection;
        private readonly ILiteCollection<VertexEntity> vertices;

        public LiteDbNeighborRepository(ILiteDatabase db) 
        {
            vertices = db.GetNamedCollection<VertexEntity>();
            collection = db.GetNamedCollection<NeighbourEntity>();
            collection.EnsureIndex(x => x.VertexId);
        }

        public NeighbourEntity AddNeighbour(NeighbourEntity neighbour)
        {
            collection.Insert(neighbour);
            return neighbour;
        }

        public IEnumerable<NeighbourEntity> AddNeighbours(IEnumerable<NeighbourEntity> neighbours)
        {
            collection.Insert(neighbours);
            return neighbours;
        }

        public bool DeleteByGraphId(int graphId)
        {
            var bsonIds = vertices.Find(x => x.GraphId == graphId)
                .Select(x => new BsonValue(x.Id)).ToArray();
            var query = Query.In(nameof(NeighbourEntity.VertexId), bsonIds);
            int deleted = collection.DeleteMany(query);
            return deleted > 0;
        }

        public bool DeleteNeighbour(int vertexId, int neighbourId)
        {
            int deleted = collection.DeleteMany(x => x.VertexId == vertexId 
                          && x.NeighbourId == neighbourId);
            return deleted == 1;
        }

        public IReadOnlyDictionary<int, IReadOnlyCollection<NeighbourEntity>> GetNeighboursForVertices(IEnumerable<int> verticesIds)
        {
            var bsonIds = verticesIds.Select(x => new BsonValue(x)).ToArray();
            var query = Query.In(nameof(NeighbourEntity.VertexId), bsonIds);
            return collection.Find(query)
                .GroupBy(x => x.VertexId)
                .ToDictionary(x => x.Key, x => (IReadOnlyCollection<NeighbourEntity>)Array.AsReadOnly(x.ToArray()))
                .AsReadOnly();
        }
    }
}
