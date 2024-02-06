using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbSubAlgorithmRepository : ISubAlgorithmRepository
    {
        private readonly ILiteCollection<SubAlgorithmEntity> collection;

        public LiteDbSubAlgorithmRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<SubAlgorithmEntity>(DbTables.SubAlgorithms);
            collection.EnsureIndex(x => x.AlgorithmId);
        }

        public bool DeleteByAlgorithmId(int algorithmId)
        {
            return collection.DeleteMany(x => x.AlgorithmId == algorithmId) > 0;
        }

        public IEnumerable<SubAlgorithmEntity> GetByAlgorithmId(int algorithmId)
        {
            return collection.Find(x => x.AlgorithmId == algorithmId);
        }

        public IEnumerable<SubAlgorithmEntity> Insert(IEnumerable<SubAlgorithmEntity> entities)
        {
            collection.InsertBulk(entities);
            return entities;
        }
    }
}
