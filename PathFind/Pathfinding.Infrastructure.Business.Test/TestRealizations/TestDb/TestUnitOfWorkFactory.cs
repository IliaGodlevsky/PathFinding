using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb
{
    public sealed class TestUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly TestUnitOfWork unitOfWork = new();

        public IUnitOfWork Create() => unitOfWork;
    }
}
