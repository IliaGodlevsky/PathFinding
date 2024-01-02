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

        public IAlgorithmsRepository AlgorithmsRepository { get; }

        public IVerticesRepository VerticesRepository { get; }

        public IRangeRepository RangeRepository { get; }

        public INeighborsRepository NeighborsRepository { get; }

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
            this.database = db;
            GraphRepository = new LiteDbGraphRepository(database);
            AlgorithmsRepository = new LiteDbAlgorithmRepository(database);
            VerticesRepository = new LiteDbVerticesRepository(database);
            NeighborsRepository = new LiteDbNeighborsRepository(database);
            RangeRepository = new LiteDbRangeRepository(database);
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

        public void SaveChanges()
        {

        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
