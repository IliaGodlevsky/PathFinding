using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Repositories.LiteDbRepositories;
using System.IO;

namespace Pathfinding.App.Console.DAL.UOF
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
            AlgorithmsRepository = new LiteDbAlgorithmRepository(database);
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

        public void CommitTransaction()
        {
            database.Commit();
        }

        public void RollbackTransaction()
        {
            database.Rollback();
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
