using System;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IUnitOfWork : IDisposable
    {
        IGraphParametresRepository GraphRepository { get; }

        IAlgorithmsRepository AlgorithmsRepository { get; }

        ISubAlgorithmRepository SubAlgorithmRepository { get; }

        IVerticesRepository VerticesRepository { get; }

        INeighborsRepository NeighborsRepository { get; }

        IRangeRepository RangeRepository { get; }

        void BeginTransaction();

        void RollbackTransaction();

        void CommitTransaction();
    }
}
