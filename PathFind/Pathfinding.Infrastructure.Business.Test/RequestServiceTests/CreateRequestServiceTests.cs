using Pathfinding.Infrastructure.Business.Test.TestRealizations;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.Infrastructure.Business.Test.RequestServiceTests
{
    [TestFixture, UnitTest]
    public class CreateRequestServiceTests
    {
        private const string GraphName = "test";

        [Test]
        public async Task CreateGraph_ValidGraph_ShouldCreate()
        {
            var service = TestRequestServiceFactory.GetForTest();
            var request = new CreateGraphRequest<TestVertex>()
            {
                Graph = TestGraph.Interface,
                Name = GraphName,
                Neighborhood = "Test",
                SmoothLevel = "Test"
            };

            var result = await service.CreateGraphAsync(request);

            Assert.Multiple(() =>
            {
                Assert.That(EntityEqualityComparer.AreEqual(request, result),
                    "Result was not equal to the original the request data");
                Assert.That(result.Id, Is.GreaterThan(0), "Result has no id");
                Assert.That(result.Graph.All(x => x.Id > 0), Is.True,
                    "Graph vertices where not assigned with an id");
            });
        }

        [Test]
        public async Task CreateGraph_ValidSerialization_ShouldCreate()
        {
            var service = TestRequestServiceFactory.GetForTest();
            var model = EntityBuilder.CreateGraphSerializationModelFull();

            var result = await service.CreateGraphAsync(model);

            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo(model.Name));
                Assert.That(result.Neighborhood, Is.EqualTo(model.Neighborhood));
                Assert.That(result.SmoothLevel, Is.EqualTo(model.SmoothLevel));
                Assert.That(result.Id, Is.GreaterThan(0));
                Assert.That(result.Graph.All(x => x.Id > 0), Is.True);
                Assert.That(result.Id, Is.GreaterThan(0));
                // TODO: add more asserts
            });
        }

        [Test]
        public async Task CreateHistory_ValidInput_ShouldCreate()
        {
            var request = EntityBuilder
                .CreatePathfindingHistoryRequest()
                .WithRandomRunHistory(2)
                .WithRandomRange()
                .WithTestGraph();

            var service = TestRequestServiceFactory.GetForTest();
            var result = await service.CreatePathfindingHistoryAsync(request);

            Assert.Multiple(() =>
            {
                Assert.That(result.Graph.Id, Is.GreaterThan(0));
                Assert.That(result.Algorithms.Count, Is.GreaterThan(0));
                //TODO: Add more asserts
            });
        }

        [Test]
        public async Task CreateRunHistory_ValidInput_ShouldCreate()
        {
            var request = EntityBuilder
                .CreateAlgorithmRunHistoryRequest()
                .WithRandomRun()
                .WithRandomSubAlgorithm(2)
                .WithRandomGraphState()
                .WithRandomStatisitics();

            var service = TestRequestServiceFactory.GetForTest();

            var result = await service.CreateRunHistoryAsync(request);

            Assert.Multiple(() =>
            {
                Assert.That(EntityEqualityComparer.AreEqual(request, result),
                    "Result is not equal to the original request");
                Assert.That(result.Run.Id, Is.GreaterThan(0));
                // TODO: Add more asserts
            });
        }

        [Test]
        public async Task CreateHistory_SerializationInput_ShouldCreate()
        {
            var model = EntityBuilder
                .CreatePathfindingHistorySerializationModel()
                .WithTestGraph()
                .WithRandomRunHistory(2)
                .WithRandomRange();

            var service = TestRequestServiceFactory.GetForTest();
            var result = await service.CreatePathfindingHistoryAsync(model);

            Assert.Multiple(() =>
            {
                Assert.That(result.Algorithms.Count, Is.GreaterThan(0));
                // TODO: Add more asserts
            });
        }

        [Test]
        public async Task CreateHistory_ModelInput_ShouldCreate()
        {
            var model = EntityBuilder
                .CreatePathfindingHistoryRequest()
                .WithRandomRange()
                .WithRandomRunHistory()
                .WithTestGraph();

            var service = TestRequestServiceFactory.GetForTest();
            var result = await service.CreatePathfindingHistoryAsync(model);

            Assert.That(EntityEqualityComparer.AreEqual(model, result));
            //TODO: Add more asserts
        }
    }
}
