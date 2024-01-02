using System;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IUnitOfWork : IDisposable
    {
        IGraphParametresRepository GraphRepository { get; }

        IAlgorithmsRepository AlgorithmsRepository { get; }

        IVerticesRepository VerticesRepository { get; }

        INeighborsRepository NeighborsRepository { get; }

        IRangeRepository RangeRepository { get; }

        void SaveChanges();

        void BeginTransaction();

        void RollbackTransaction();

        void CommitTransaction();
    }
}
