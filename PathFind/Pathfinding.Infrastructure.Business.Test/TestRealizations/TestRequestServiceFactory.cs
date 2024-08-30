using Pathfinding.Infrastructure.Data.InMemory;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations
{
    internal static class TestRequestServiceFactory
    {
        public static RequestService<TestVertex> GetForTest()
        {
            var unitOfWork = new InMemoryUnitOfWorkFactory();
            var mapper = TestMapper.Interface.Mapper;
            return new RequestService<TestVertex>(mapper, unitOfWork);
        }
    }
}
