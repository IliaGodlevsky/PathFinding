using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbStatisticsRepository : IStatisticsRepository
    {
        private readonly ILiteCollection<StatisticsEntity> collection;

        public LiteDbStatisticsRepository(ILiteDatabase db) 
        {
            collection = db.GetCollection<StatisticsEntity>(DbTables.Statistics);
        }

        public bool DeleteByRunId(int runId)
        {
            return collection.DeleteMany(x => x.AlgorithmRunId == runId) > 0;
        }

        public StatisticsEntity GetByAlgorithmRunId(int runId)
        {
            return collection.FindOne(x => x.AlgorithmRunId == runId);
        }

        public IEnumerable<StatisticsEntity> GetByRunIds(IEnumerable<int> runIds)
        {
            var ids = runIds.Select(x => new BsonValue(x)).ToArray();
            var query = Query.In(nameof(StatisticsEntity.AlgorithmRunId), ids);
            return collection.Find(query).ToList();
        }

        public StatisticsEntity Insert(StatisticsEntity entity)
        {
            collection.Insert(entity);
            return entity;
        }
    }
}
