using LiteDB;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Infrastructure.Data.LiteDb.Repositories;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Data.LiteDb
{
    internal sealed class LiteDbUnitOfWork : IUnitOfWork
    {
        private readonly ILiteDatabase database;

        public IGraphParametresRepository GraphRepository { get; }

        public ISubAlgorithmRepository SubAlgorithmRepository { get; }

        public IAlgorithmsRepository AlgorithmsRepository { get; }

        public IVerticesRepository VerticesRepository { get; }

        public IRangeRepository RangeRepository { get; }

        public INeighborsRepository NeighborsRepository { get; }

        public IStatisticsRepository StatisticsRepository { get; }

        public IGraphStateRepository GraphStateRepository { get; }

        public IAlgorithmRunRepository RunRepository { get; }

        public LiteDbUnitOfWork(string connectionString)
            : this(new LiteDatabase(connectionString))
        {
        }

        public LiteDbUnitOfWork(Stream stream)
            : this(new LiteDatabase(stream))
        {
        }

        public LiteDbUnitOfWork(ILiteDatabase db)
        {
            database = db;
            SubAlgorithmRepository = new LiteDbSubAlgorithmRepository(database);
            GraphRepository = new LiteDbGraphRepository(database);
            AlgorithmsRepository = new LiteDbAlgorithmsRepository(database);
            VerticesRepository = new LiteDbVerticesRepository(database);
            NeighborsRepository = new LiteDbNeighborsRepository(database);
            RangeRepository = new LiteDbRangeRepository(database);
            StatisticsRepository = new LiteDbStatisticsRepository(database);
            GraphStateRepository = new LiteDbGraphStateRepository(database);
            RunRepository = new LiteDbAlgorithmRunRepository(database);
        }

        public void BeginTransaction()
        {
            database.BeginTrans();
        }

        public void Commit()
        {
            database.Commit();
        }

        public void Rollback()
        {
            database.Rollback();
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public async Task RollbackAsync(CancellationToken token = default)
        {
            database.Rollback();
            await Task.CompletedTask;
        }

        public async Task CommitAsync(CancellationToken token = default)
        {
            database.Commit();
            await Task.CompletedTask;
        }
    }
}
