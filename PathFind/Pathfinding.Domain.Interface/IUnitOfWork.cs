using Pathfinding.Domain.Interface.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGraphParametresRepository GraphRepository { get; }

        IAlgorithmsRepository AlgorithmsRepository { get; }

        ISubAlgorithmRepository SubAlgorithmRepository { get; }

        IVerticesRepository VerticesRepository { get; }

        INeighborsRepository NeighborsRepository { get; }

        IRangeRepository RangeRepository { get; }

        IStatisticsRepository StatisticsRepository { get; }

        IGraphStateRepository GraphStateRepository { get; }

        IAlgorithmRunRepository RunRepository { get; }

        void BeginTransaction();

        void Rollback();

        void Commit();

        Task RollbackAsync(CancellationToken token = default);

        Task CommitAsync(CancellationToken token = default);
    }
}
