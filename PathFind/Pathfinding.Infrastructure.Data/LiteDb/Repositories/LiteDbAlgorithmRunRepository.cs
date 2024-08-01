using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    public sealed class LiteDbAlgorithmRunRepository : IAlgorithmRunRepository
    {
        private const string AlgorithmRunId = "AlgorithmRunId";

        private readonly ILiteCollection<AlgorithmRun> collection;
        private readonly ILiteCollection<Statistics> statistics;
        private readonly ILiteCollection<GraphState> graphStates;
        private readonly ILiteCollection<SubAlgorithm> subAlgorithms;

        public LiteDbAlgorithmRunRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<AlgorithmRun>(DbTables.AlgorithmRuns);
            statistics = db.GetCollection<Statistics>(DbTables.Statistics);
            graphStates = db.GetCollection<GraphState>(DbTables.GraphStates);
            subAlgorithms = db.GetCollection<SubAlgorithm>(DbTables.SubAlgorithms);
        }

        public async Task<AlgorithmRun> CreateAsync(AlgorithmRun entity,
            CancellationToken token = default)
        {
            collection.Insert(entity);
            return await Task.FromResult(entity);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            return await Task.Run(async () =>
            {
                var runs = await ReadByGraphIdAsync(graphId, token);
                var ids = runs
                    .Select(x => new BsonValue(x.Id))
                    .ToArray();
                var query = Query.In(AlgorithmRunId, ids);
                statistics.DeleteMany(query);
                graphStates.DeleteMany(query);
                subAlgorithms.DeleteMany(query);
                return collection.DeleteMany(x => x.GraphId == graphId) > 0;
            }, token);
        }

        public async Task<IEnumerable<AlgorithmRun>> ReadByGraphIdAsync(int graphId, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                var result = collection
                    .Find(x => x.GraphId == graphId)
                    .ToList();
                return result;
            }, token);
        }

        public async Task<int> ReadCount(int graphId, CancellationToken token = default)
        {
            return await Task.Run(() => collection.Count(x => x.GraphId == graphId), token);
        }
    }
}
