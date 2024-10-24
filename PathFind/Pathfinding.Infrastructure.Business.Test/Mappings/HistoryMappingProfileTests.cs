using AutoMapper;
using Pathfinding.Infrastructure.Business.Test.TestRealizations;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.Infrastructure.Business.Test.Mappings
{
    [TestFixture, UnitTest]
    public class HistoryMappingProfileTests
    {
        private readonly IMapper mapper = TestMapper.Instance.Mapper;

        [Test]
        public void CreateHistoryRequestToModel_ValidInput_ShouldMap()
        {
            var request = EntityBuilder
                .CreatePathfindingHistoryRequest()
                .WithRandomRange()
                .WithTestGraph()
                .WithRandomRunHistory(2);

            var mapped = mapper.Map<PathfindingHistoryModel<TestVertex>>(request);

            Assert.That(EntityEqualityComparer.AreEqual(request, mapped),
                "Mapped value is not equal to the original one");
        }
    }
}
