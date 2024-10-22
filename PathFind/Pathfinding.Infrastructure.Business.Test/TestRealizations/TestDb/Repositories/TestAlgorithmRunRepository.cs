using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb.Repositories
{
    internal sealed class TestAlgorithmRunRepository : IAlgorithmRunRepository
    {
        public async Task<AlgorithmRun> CreateAsync(AlgorithmRun entity,
            CancellationToken token = default)
        {
            entity.Id = 1;
            return await Task.FromResult(entity);
        }

        public async Task<bool> DeleteByGraphIdAsync(int graphId,
            CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<AlgorithmRun>> ReadByGraphIdAsync(int graphId,
            CancellationToken token = default)
        {
            var result = new AlgorithmRun()
            {
                Id = 1,
                GraphId = graphId,
                AlgorithmId = "TEST"
            };
            return await Task.FromResult(result.Enumerate());
        }

        public async Task<bool> DeleteByRunIdsAsync(IEnumerable<int> runIds, CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }

        public async Task<AlgorithmRun> ReadAsync(int id, CancellationToken token = default)
        {
            return await Task.FromResult(new AlgorithmRun() 
            { 
                Id = id, 
                GraphId = 1, 
                AlgorithmId = "TEST"
            });
        }
    }
}
