using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb.Repositories
{
    internal sealed class TestGraphStateRepository : IGraphStateRepository
    {
        public async Task<GraphState> CreateAsync(GraphState entity,
            CancellationToken token = default)
        {
            entity.Id = 1;
            return await Task.FromResult(entity);
        }

        public async Task<GraphState> ReadByRunIdAsync(int runId,
            CancellationToken token = default)
        {
            var state = new GraphState()
            {
                Id = 1,
                AlgorithmRunId = 1,
                Range = Array.Empty<byte>(),
                Regulars = Array.Empty<byte>(),
                Obstacles = Array.Empty<byte>(),
                Costs = Array.Empty<byte>()
            };
            return await Task.FromResult(state);
        }
    }
}
