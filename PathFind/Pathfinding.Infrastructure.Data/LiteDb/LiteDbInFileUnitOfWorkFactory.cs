using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;

namespace Pathfinding.Infrastructure.Data.LiteDb
{
    public sealed class LiteDbInFileUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string connectionString;

        public LiteDbInFileUnitOfWorkFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IUnitOfWork Create()
        {
            return new LiteDbUnitOfWork(connectionString);
        }
    }
}
