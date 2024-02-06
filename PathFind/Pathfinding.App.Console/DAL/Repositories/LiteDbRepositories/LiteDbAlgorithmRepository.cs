using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbAlgorithmRepository : IAlgorithmsRepository
    {
        private readonly ILiteCollection<AlgorithmEntity> collection;
        private readonly ILiteCollection<SubAlgorithmEntity> subAlgorithms;

        public LiteDbAlgorithmRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<AlgorithmEntity>(DbTables.Algorithms);
            subAlgorithms = db.GetCollection<SubAlgorithmEntity>(DbTables.SubAlgorithms);
            collection.EnsureIndex(x => x.GraphId);
            subAlgorithms.EnsureIndex(x => x.AlgorithmId);
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
            var algorithms = collection.Find(x => x.GraphId == graphId);
            var ids = algorithms.Select(x => new BsonValue(x.Id)).ToArray();
            var query = Query.In(nameof(SubAlgorithmEntity.AlgorithmId), ids);
            subAlgorithms.DeleteMany(query);
            int count = collection.DeleteMany(x => x.GraphId == graphId);
            return count > 0;
        }

        public IEnumerable<AlgorithmEntity> GetByGraphId(int graphId)
        {
            return collection.Find(x => x.GraphId == graphId);
        }
    }
}
