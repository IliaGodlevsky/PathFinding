using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    public sealed class LiteDbStatisticsRepository : IStatisticsRepository
    {
        private readonly ILiteCollection<Statistics> collection;

        public LiteDbStatisticsRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<Statistics>(DbTables.Statistics);
        }

        public async Task<Statistics> CreateAsync(Statistics entity, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                collection.Insert(entity);
                return entity;
            }, token);
        }

        public async Task<Statistics> ReadByAlgorithmRunIdAsync(int runId, CancellationToken token = default)
        {
            return await Task.Run(() => collection.FindOne(x => x.AlgorithmRunId == runId), token);
        }

        public async Task<IEnumerable<Statistics>> ReadByRunIdsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                var ids = runIds.Select(x => new BsonValue(x)).ToArray();
                var query = Query.In(nameof(Statistics.AlgorithmRunId), ids);
                return collection.Find(query).ToArray();
            }, token);
        }
    }
}
