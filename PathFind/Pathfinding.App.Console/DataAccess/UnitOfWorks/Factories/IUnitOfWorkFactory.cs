namespace Pathfinding.App.Console.DataAccess.UnitOfWorks.Factories
{
    internal interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }

    internal sealed class SqliteUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new SqliteUnitOfWork();
        }
    }

    internal sealed class LiteDbUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new LiteDbUnitOfWork();
        }
    }

    internal sealed class InMemoryUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly InMemoryUnitOfWork instance = new();

        public IUnitOfWork Create()
        {
            return instance;
        }
    }
}
