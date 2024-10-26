using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    internal sealed class LiteDbAlgorithmRunRepository : IAlgorithmRunRepository
    {
        private const string AlgorithmRunId = "AlgorithmRunId";

        private readonly ILiteCollection<AlgorithmRun> collection;
        private readonly ILiteCollection<Statistics> statistics;
        private readonly ILiteCollection<GraphState> graphStates;

        public LiteDbAlgorithmRunRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<AlgorithmRun>(DbTables.AlgorithmRuns);
            statistics = db.GetCollection<Statistics>(DbTables.Statistics);
            graphStates = db.GetCollection<GraphState>(DbTables.GraphStates);
        }

        public async Task<AlgorithmRun> CreateAsync(AlgorithmRun entity,
            CancellationToken token = default)
        {
            collection.Insert(entity);
            return await Task.FromResult(entity);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            var runs = await ReadByGraphIdAsync(graphId, token);
            return await DeleteByRunIdsAsync(runs.Select(x => x.Id), token);

        }

        public async Task<bool> DeleteByRunIdsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            var ids = runIds.Select(x => new BsonValue(x)).ToArray();
            var query = Query.In(AlgorithmRunId, ids);
            statistics.DeleteMany(query);
            graphStates.DeleteMany(query);
            var runQuery = Query.In("_id", ids);
            return await Task.FromResult(collection.DeleteMany(runQuery) > 0);
        }

        public async Task<IEnumerable<AlgorithmRun>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            var result = collection
                .Find(x => x.GraphId == graphId)
                .ToList();
            return await Task.FromResult(result);
        }

        public async Task<AlgorithmRun> ReadAsync(int runId, CancellationToken token = default)
        {
            var result = collection.FindById(runId);
            return await Task.FromResult(result);
        }
    }
}
