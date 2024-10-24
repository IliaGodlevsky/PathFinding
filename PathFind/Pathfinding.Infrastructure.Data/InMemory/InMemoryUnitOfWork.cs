using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Infrastructure.Data.InMemory.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.InMemory
{
    public sealed class InMemoryUnitOfWork : IUnitOfWork
    {
        public IGraphParametresRepository GraphRepository { get; }

        public ISubAlgorithmRepository SubAlgorithmRepository { get; }

        public IVerticesRepository VerticesRepository { get; }

        public IRangeRepository RangeRepository { get; }

        public IStatisticsRepository StatisticsRepository { get; }

        public IGraphStateRepository GraphStateRepository { get; }

        public IAlgorithmRunRepository RunRepository { get; }

        public InMemoryUnitOfWork()
        {
            var graphState = new InMemoryGraphStateRepository();
            var vertices = new InMemoryVerticesRepository();
            var range = new InMemoryRangeRepository();
            var subs = new InMemorySubAlgorithmRepository();
            var statistics = new InMemoryStatisicsRepository();
            var runs = new InMemoryAlgorithmRunRepository(statistics, graphState, subs);
            GraphStateRepository = graphState;
            VerticesRepository = vertices;
            RangeRepository = range;
            SubAlgorithmRepository = subs;
            StatisticsRepository = statistics;
            RunRepository = runs;
            GraphRepository = new InMemoryGraphParametresRepository(runs, range, vertices);
        }

        public void BeginTransaction()
        {

        }

        public void Commit()
        {

        }

        public async Task CommitAsync(CancellationToken token = default)
        {
            await Task.CompletedTask;
        }

        public void Dispose()
        {

        }

        public void Rollback()
        {

        }

        public async Task RollbackAsync(CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
