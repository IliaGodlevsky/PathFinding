using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    internal sealed class LiteDbSubAlgorithmRepository : ISubAlgorithmRepository
    {
        private readonly ILiteCollection<SubAlgorithm> collection;

        public LiteDbSubAlgorithmRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<SubAlgorithm>(DbTables.SubAlgorithms);
        }

        public async Task<IEnumerable<SubAlgorithm>> CreateAsync(IEnumerable<SubAlgorithm> entities, CancellationToken token = default)
        {
            collection.InsertBulk(entities);
            return await Task.FromResult(entities);
        }

        public async Task<IEnumerable<SubAlgorithm>> ReadByAlgorithmRunIdAsync(int runId, CancellationToken token = default)
        {
            await Task.CompletedTask;
            return collection.Query()
                .Where(x => x.AlgorithmRunId == runId)
                .OrderBy(x => x.Order)
                .ToEnumerable();
        }
    }
}
