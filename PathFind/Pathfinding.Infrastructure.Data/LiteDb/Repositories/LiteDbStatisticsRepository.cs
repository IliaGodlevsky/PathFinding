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

        public Task<IEnumerable<Statistics>> CreateAsync(IEnumerable<Statistics> statistics, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteByGraphId(int graphId, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteByIdsAsync(IEnumerable<int> ids, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Statistics> ReadByAlgorithmRunIdAsync(int runId, CancellationToken token = default)
        {
            return await Task.FromResult(collection.FindOne(x => x.Id == runId));
        }

        public Task<IEnumerable<Statistics>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Statistics> ReadByIdAsync(int runId, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Statistics>> ReadByRunIdsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            var ids = runIds.Select(x => new BsonValue(x)).ToArray();
            var query = Query.In(nameof(Statistics.Id), ids);
            return await Task.FromResult(collection.Find(query).ToArray());
        }

        public Task<int> ReadStatisticsCountAsync(int graphId, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
