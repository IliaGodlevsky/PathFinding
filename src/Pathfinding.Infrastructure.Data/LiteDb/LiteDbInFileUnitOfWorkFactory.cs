using LiteDB;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;

namespace Pathfinding.Infrastructure.Data.LiteDb
{
    public sealed class LiteDbInFileUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ConnectionString connectionString;

        public LiteDbInFileUnitOfWorkFactory(ConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public LiteDbInFileUnitOfWorkFactory(string connectionString)
            : this(new ConnectionString(connectionString))
        {

        }

        public IUnitOfWork Create()
        {
            return new LiteDbUnitOfWork(connectionString);
        }
    }
}
