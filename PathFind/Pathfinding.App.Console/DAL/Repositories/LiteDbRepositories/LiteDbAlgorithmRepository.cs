using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbAlgorithmRepository : IAlgorithmsRepository
    {
        private readonly ILiteCollection<AlgorithmEntity> collection;

        public LiteDbAlgorithmRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<AlgorithmEntity>(DbTables.Algorithms);
            collection.EnsureIndex(x => x.GraphId);
        }

        public IEnumerable<AlgorithmEntity> Insert(IEnumerable<AlgorithmEntity> entity)
        {
            collection.InsertBulk(entity);
            return entity;
        }

        public AlgorithmEntity Insert(AlgorithmEntity entity)
        {
            collection.Insert(entity);
            return entity;
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
