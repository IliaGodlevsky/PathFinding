using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbSubAlgorithmRepository : ISubAlgorithmRepository
    {
        private readonly ILiteCollection<SubAlgorithmEntity> collection;

        public LiteDbSubAlgorithmRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<SubAlgorithmEntity>(DbTables.SubAlgorithms);
        }

        public IEnumerable<SubAlgorithmEntity> GetByAlgorithmRunId(int runId)
        {
            return collection.Find(x => x.AlgorithmRunId == runId);
        }

        public IEnumerable<SubAlgorithmEntity> Insert(IEnumerable<SubAlgorithmEntity> entities)
        {
            collection.InsertBulk(entities);
            return entities;
        }
    }
}
