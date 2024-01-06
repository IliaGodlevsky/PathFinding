using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbAlgorithmRepository : IAlgorithmsRepository
    {
        private readonly ILiteCollection<AlgorithmEntity> collection;

        public LiteDbAlgorithmRepository(ILiteDatabase db)
        {
            collection = db.GetNamedCollection<AlgorithmEntity>();
            collection.EnsureIndex(x => x.GraphId);
        }

        public int AddMany(IEnumerable<AlgorithmEntity> entity)
        {
            return collection.InsertBulk(entity);
        }

        public void AddOne(AlgorithmEntity entity)
        {
            collection.Insert(entity);
        }

        public bool DeleteByGraphId(int graphId)
        {
            int count = collection.DeleteMany(x => x.GraphId == graphId);
            return count > 0;
        }

        public IEnumerable<AlgorithmEntity> GetByGraphId(int graphId)
        {
            return collection.Find(x => x.GraphId == graphId);
        }
    }
}
