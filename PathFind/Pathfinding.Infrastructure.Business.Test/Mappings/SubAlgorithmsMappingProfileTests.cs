using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Test.TestRealizations;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.Infrastructure.Business.Test.Mappings
{
    [TestFixture, UnitTest]
    public class SubAlgorithmsMappingProfileTests
    {
        private readonly IMapper mapper;

        public SubAlgorithmsMappingProfileTests()
        {
            mapper = TestMapper.Interface.Mapper;
        }

        [Test]
        public void CreateRequestToModel_ValidData_ShouldMap()
        {
            var createRequest = EntityBuilder
                .CreateSubAlgorithmRequest()
                .WithRandomVisited(2)
                .WithRandomPath()
                .WithOrder(1)
                .WithRunId(1);

            var mapped = mapper.Map<SubAlgorithmModel>(createRequest);

            Assert.That(EntityEqualityComparer.AreEqual(createRequest, mapped), Is.True,
                "Mapped entity is not equal to original one");
        }

        [Test]
        public void CreateRequestToEntity_ValidData_ShouldMap()
        {
            var createRequest = EntityBuilder
                .CreateSubAlgorithmRequest()
                .WithRandomVisited(2)
                .WithRandomPath()
                .WithOrder(1)
                .WithRunId(1);

            var mapped = mapper.Map<SubAlgorithm>(createRequest);

            Assert.Multiple(() =>
            {
                Assert.That(mapped.Path.Length, Is.GreaterThan(0),
                    "Path of mapped value is empty");
                Assert.That(mapped.Order, Is.EqualTo(createRequest.Order),
                    "The order of mapped value is not equal to the original one");
                Assert.That(mapped.AlgorithmRunId, Is.EqualTo(createRequest.AlgorithmRunId),
                    "The algorithm run id of mapped value is not equal to the original one");
                Assert.That(mapped.Visited.Length, Is.GreaterThan(0),
                    "The visited coordinates are empty");
            });
        }
    }
}
