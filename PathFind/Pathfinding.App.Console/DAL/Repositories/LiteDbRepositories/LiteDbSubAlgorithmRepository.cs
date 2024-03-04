using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbSubAlgorithmRepository(ILiteDatabase db) : ISubAlgorithmRepository
    {
        private readonly ILiteCollection<SubAlgorithmEntity> collection = db.GetCollection<SubAlgorithmEntity>(DbTables.SubAlgorithms);

        public IEnumerable<SubAlgorithmEntity> GetByAlgorithmRunId(int runId)
        {
            return collection.Query()
                .Where(x => x.AlgorithmRunId == runId)
                .OrderBy(x => x.Order)
                .ToEnumerable();
        }

        public IEnumerable<SubAlgorithmEntity> Insert(IEnumerable<SubAlgorithmEntity> entities)
        {
            collection.InsertBulk(entities);
            return entities;
        }
    }
}
