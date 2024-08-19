# nullable disable
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;

namespace Pathfinding.Infrastructure.Business.Test.Mock.TestUnitOfWork
{
    internal sealed class TestGraphStateRepository : IGraphStateRepository
    {
        private int id = 0;

        private readonly HashSet<GraphState> set = new(EntityComparer<int>.Instance);

        public async Task<GraphState> CreateAsync(GraphState entity, CancellationToken token = default)
        {
            entity.Id = ++id;
            set.Add(entity);
            return await Task.FromResult(entity);
        }

        public async Task<GraphState> ReadByRunIdAsync(int runId, CancellationToken token = default)
        {
            return await Task.FromResult(set.FirstOrDefault(x => x.AlgorithmRunId == runId));
        }
    }
}
