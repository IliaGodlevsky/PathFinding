using Pathfinding.Infrastructure.Business.Test.Mock;
using Pathfinding.Infrastructure.Business.Test.Mock.TestUnitOfWork;
using Pathfinding.Infrastructure.Data.LiteDb;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Service.Interface.Requests.Read;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using Pathfinding.Shared.Random;

namespace Pathfinding.Infrastructure.Business.Test
{
    [TestFixture]
    public class RequestServiceTests
    {
        private const string GraphName = "test";

        [Test]
        public async Task CreateGraph_ValidGraph_ShouldCreate()
        {
            var unitOfWork = new TestUnitOfWork();
            var service = new RequestService<TestVertex>(TestMapper.Interface.Mapper, () => unitOfWork);
            var request = new CreateGraphRequest<TestVertex>() 
            {
                Graph = TestGraph.Interface,
                Name = GraphName
            };

            var result = await service.CreateGraphAsync(request);

            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo(GraphName));
                Assert.That(result.Id, Is.GreaterThan(0));
                Assert.IsTrue(result.Graph.All(x => x.Id > 0));
                Assert.That(result.Id, Is.GreaterThan(0));
            });
        }

        [Test]
        public async Task CreateGraph_ValidSerialization_ShouldCreate()
        {
            var unitOfWork = new TestUnitOfWork();
            var service = new RequestService<TestVertex>(TestMapper.Interface.Mapper, () => unitOfWork);
            var request = new GraphSerializationModel()
            {
                Name = GraphName,
                DimensionSizes = TestGraph.Instance.DimensionsSizes,
                Vertices = TestMapper.Instance.Mapper
                    .Map<VertexSerializationModel[]>(TestGraph.Interface.ToArray())
            };

            var result = await service.CreateGraphAsync(request);

            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo(GraphName));
                Assert.That(result.Id, Is.GreaterThan(0));
                Assert.IsTrue(result.Graph.All(x => x.Id > 0));
                Assert.That(result.Id, Is.GreaterThan(0));
            });
        }

        [Test]
        public async Task CreateHistory_ValidInput_ShouldCreate()
        {
            var unitOfWork = new TestUnitOfWork();
            var range = new InclusiveValueRange<int>(30, 1);
            var random = new CongruentialRandom();
            var service = new RequestService<TestVertex>(TestMapper.Interface.Mapper, () => unitOfWork);
            var subRequest = new CreatePathfindingHistoryRequest<TestVertex>()
            {
                Graph = new CreateGraphRequest<TestVertex>()
                {
                    Graph = TestGraph.Interface,
                    Name = GraphName
                },
                Range = random.GenerateCoordinates(range, 2, 7).ToList(),
                Algorithms = new List<CreateAlgorithmRunHistoryRequest>()
                {
                    new CreateAlgorithmRunHistoryRequest()
                    {
                        Run = new CreateAlgorithmRunRequest()
                        {
                            AlgorithmId = "Algorithm",
                        },
                        GraphState = new CreateGraphStateRequest()
                        {
                            Obstacles = random.GenerateCoordinates(range, 2, 25).ToList(),
                            Costs = random.GenerateNumbers((9, 1), 30).ToList(),
                            Range = random.GenerateCoordinates(range, 2, 5).ToList()
                        },
                        SubAlgorithms = new List<CreateSubAlgorithmRequest>()
                        {
                            new CreateSubAlgorithmRequest()
                            {
                                Order = 1,
                                Visited = new List<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)>()
                                {
                                    { (new Coordinate(0, 0), random.GenerateCoordinates(range, 2, 15).ToList()) },
                                    { (new Coordinate(1, 2), random.GenerateCoordinates(range, 2, 15).ToList()) },
                                },
                                Path = random.GenerateCoordinates(range, 2, 50).ToList()
                            },
                            new CreateSubAlgorithmRequest()
                            {
                                Order = 2,
                                Visited = new List<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)>()
                                {
                                    { (new Coordinate(1, 1), random.GenerateCoordinates(range, 2, 15).ToList()) },
                                    { (new Coordinate(1, 15), random.GenerateCoordinates(range, 2, 15).ToList()) },
                                },
                                Path = random.GenerateCoordinates(range, 2, 50).ToList()
                            }
                        },
                        Statistics = new RunStatisticsModel()
                        {
                            AlgorithmId = "Algorithms",
                            AlgorithmSpeed = TimeSpan.FromMilliseconds(2),
                            Visited = 5,
                            Cost = 100,
                            StepRule = "Default",
                            Heuristics = "Euclidian",
                            ResultStatus = "Success",
                            Elapsed = TimeSpan.FromMilliseconds(5),
                            Steps = 20,
                            Spread = 3
                        }
                    }
                }
            };

            var request = new CreatePathfindingHistoriesRequest<TestVertex>()
            {
                Models = new List<CreatePathfindingHistoryRequest<TestVertex>>() { subRequest }
            };

            var result = (await service.CreatePathfindingHistoryAsync(request)).Models.First();

            Assert.Multiple(() =>
            {
                Assert.That(result.Graph.Id, Is.GreaterThan(0));
                Assert.That(result.Algorithms.Count, Is.GreaterThan(0));
                //TODO: Add new asserts
            });
        }
    }
}
