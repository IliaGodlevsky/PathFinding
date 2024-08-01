using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using System.IO;

namespace Pathfinding.Infrastructure.Data.LiteDb
{
    public sealed class LiteDbInMemoryUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static readonly MemoryStream Memory = new MemoryStream();

        public IUnitOfWork Create() => new LiteDbUnitOfWork(Memory);
    }
}
