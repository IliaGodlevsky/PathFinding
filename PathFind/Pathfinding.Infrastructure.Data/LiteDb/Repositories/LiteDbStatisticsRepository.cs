using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    internal sealed class LiteDbStatisticsRepository : IStatisticsRepository
    {
        private readonly ILiteCollection<Statistics> collection;

        public LiteDbStatisticsRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<Statistics>(DbTables.Statistics);
        }

        public async Task<Statistics> CreateAsync(Statistics entity, CancellationToken token = default)
        {
            collection.Insert(entity);
            return await Task.FromResult(entity);
        }

        public async Task<Statistics> ReadByAlgorithmRunIdAsync(int runId, CancellationToken token = default)
        {
            return await Task.FromResult(collection.FindOne(x => x.AlgorithmRunId == runId));
        }

        public async Task<IEnumerable<Statistics>> ReadByRunIdsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            var ids = runIds.Select(x => new BsonValue(x)).ToArray();
            var query = Query.In(nameof(Statistics.AlgorithmRunId), ids);
            return await Task.FromResult(collection.Find(query).ToArray());
        }
    }
}
