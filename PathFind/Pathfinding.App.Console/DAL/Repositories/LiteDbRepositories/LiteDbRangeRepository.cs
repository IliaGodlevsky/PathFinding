using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbRangeRepository : IRangeRepository
    {
        private readonly ILiteCollection<RangeEntity> collection;

        public LiteDbRangeRepository(ILiteDatabase db)
        {
            collection = db.GetNamedCollection<RangeEntity>();
            collection.EnsureIndex(x => x.GraphId);
        }

        public RangeEntity AddRange(RangeEntity entity)
        {
            collection.Insert(entity);
            return entity;
        }

        public IEnumerable<RangeEntity> AddRange(IEnumerable<RangeEntity> entities)
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
            return collection.Find(x => x.GraphId == graphId);
        }

        public RangeEntity GetByVertexId(int vertexId)
        {
            return collection.FindOne(x => x.VertexId == vertexId);
        }

        public bool Update(RangeEntity entity)
        {
            return collection.Update(entity);
        }
    }
}
