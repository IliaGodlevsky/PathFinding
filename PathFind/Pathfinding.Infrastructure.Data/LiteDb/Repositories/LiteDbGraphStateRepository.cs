using LiteDB;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb.Repositories
{
    public sealed class LiteDbGraphStateRepository : IGraphStateRepository
    {
        private readonly ILiteCollection<GraphState> collection;

        public LiteDbGraphStateRepository(ILiteDatabase db)
        {
            collection = db.GetCollection<GraphState>(DbTables.GraphStates);
        }

        public async Task<GraphState> CreateAsync(GraphState entity, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                collection.Insert(entity);
                return entity;
            }, token);
        }

        public async Task<GraphState> ReadByRunIdAsync(int runId, CancellationToken token = default)
        {
            return await Task.Run(() => collection.FindOne(x => x.AlgorithmRunId == runId), token);
        }
    }
}
