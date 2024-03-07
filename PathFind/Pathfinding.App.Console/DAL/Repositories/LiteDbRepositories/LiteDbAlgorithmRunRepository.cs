using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories
{
    internal sealed class LiteDbAlgorithmRunRepository(ILiteDatabase db)
        : IAlgorithmRunRepository
    {
        private readonly ILiteCollection<AlgorithmRunEntity> collection = db.GetCollection<AlgorithmRunEntity>(DbTables.AlgorithmRuns);
        private readonly ILiteCollection<StatisticsEntity> statistics = db.GetCollection<StatisticsEntity>(DbTables.Statistics);
        private readonly ILiteCollection<GraphStateEntity> graphStates = db.GetCollection<GraphStateEntity>(DbTables.GraphStates);
        private readonly ILiteCollection<SubAlgorithmEntity> subAlgorithms = db.GetCollection<SubAlgorithmEntity>(DbTables.SubAlgorithms);

        public bool DeleteByGraphId(int graphId)
        {
            var runs = GetByGraphId(graphId);
            var ids = runs.Select(x => new BsonValue(x.Id)).ToArray();
            var query = Query.In("AlgorithmRunId", ids);
            statistics.DeleteMany(query);
            graphStates.DeleteMany(query);
            subAlgorithms.DeleteMany(query);
            return collection.DeleteMany(x => x.GraphId == graphId) > 0;
        }

        public IEnumerable<AlgorithmRunEntity> GetByGraphId(int graphId)
        {
            var result = collection.Find(x => x.GraphId == graphId).ToList();
            return result;
        }

        public int GetCount(int graphId)
        {
            return collection.Find(x => x.GraphId == graphId).Count();
        }

        public AlgorithmRunEntity Insert(AlgorithmRunEntity entity)
        {
            collection.Insert(entity);
            return entity;
        }
    }
}
