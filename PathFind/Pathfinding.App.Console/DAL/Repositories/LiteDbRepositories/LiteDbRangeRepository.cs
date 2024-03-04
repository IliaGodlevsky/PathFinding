using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbRangeRepository(ILiteDatabase db) : IRangeRepository
    {
        private readonly ILiteCollection<RangeEntity> collection = db.GetCollection<RangeEntity>(DbTables.Ranges);

        public IEnumerable<RangeEntity> Insert(IEnumerable<RangeEntity> entities)
        {
            collection.Insert(entities);
            return entities;
        }

        public bool DeleteByGraphId(int graphId)
        {
            int deleted = collection.DeleteMany(x => x.GraphId == graphId);
            return deleted > 0;
        }

        public bool DeleteByVertexId(int vertexId)
        {
            int deleted = collection.DeleteMany(x => x.VertexId == vertexId);
            return deleted > 0;
        }

        public IEnumerable<RangeEntity> GetByGraphId(int graphId)
        {
            return collection.Query()
                .Where(x => x.GraphId == graphId)
                .OrderBy(x => x.Order)
                .ToEnumerable();
        }

        public bool Update(IEnumerable<RangeEntity> entities)
        {
            collection.Update(entities);
            return true;
        }

        public IEnumerable<RangeEntity> GetByVerticesIds(IEnumerable<int> verticesIds)
        {
            var ids = verticesIds.Select(x => new BsonValue(x)).ToArray();
            var query = Query.In(nameof(RangeEntity.VertexId), ids);
            return collection.Find(query).OrderBy(x => x.Order);
        }
    }
}
