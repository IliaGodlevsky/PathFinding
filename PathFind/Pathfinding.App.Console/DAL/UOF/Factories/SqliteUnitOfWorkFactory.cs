using Pathfinding.App.Console.DAL.Interface;

namespace Pathfinding.App.Console.DAL.UOF.Factories
{
    internal sealed class SqliteUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create() => new SqliteUnitOfWork();
    }
}
