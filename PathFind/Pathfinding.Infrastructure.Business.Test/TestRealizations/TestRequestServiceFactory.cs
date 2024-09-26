using Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations
{
    internal static class TestRequestServiceFactory
    {
        public static RequestService<TestVertex> GetForTest()
        {
            var unitOfWork = new TestUnitOfWorkFactory();
            var mapper = TestMapper.Interface.Mapper;
            return new RequestService<TestVertex>(mapper, unitOfWork);
        }
    }
}
