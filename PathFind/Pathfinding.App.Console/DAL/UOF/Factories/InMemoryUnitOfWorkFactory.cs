using Pathfinding.App.Console.DAL.Interface;
using System.IO;

namespace Pathfinding.App.Console.DAL.UOF.Factories
{
    internal sealed class InMemoryUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static readonly MemoryStream Memory = new();

        public IUnitOfWork Create() => new LiteDbUnitOfWork(Memory);
    }
}
