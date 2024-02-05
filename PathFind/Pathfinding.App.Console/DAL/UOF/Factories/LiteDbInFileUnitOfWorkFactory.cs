using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.DAL.UOF.Factories
{
    internal sealed class LiteDbInFileUnitOfWorkFactory : IUnitOfWorkFactory
    {
        readonly string ConnectionString = Parametres.Default.LiteDbConnectionString;

        public IUnitOfWork Create() => new LiteDbUnitOfWork(ConnectionString);
    }
}