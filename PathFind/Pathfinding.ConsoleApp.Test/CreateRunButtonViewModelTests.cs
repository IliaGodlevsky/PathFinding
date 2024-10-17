using CommunityToolkit.Mvvm.Messaging;
using Moq;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.Model.Factories;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Logging.Interface;
using Pathfinding.Logging.Loggers;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using Pathfinding.Shared.Random;
using Pathfinding.TestUtils.Attributes;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Test
{
    [TestFixture, UnitTest]
    internal class CreateRunButtonViewModelTests
    {
        private sealed class TestCreateRunButtonViewModel : PathfindingProcessViewModel
        {
            public TestCreateRunButtonViewModel(IRequestService<GraphVertexModel> service,
                IMessenger messenger, ILog logger) : base(service, messenger, logger)
            {
            }

            public override string AlgorithmId => "Test";

            protected override PathfindingProcess GetAlgorithm(IEnumerable<GraphVertexModel> pathfindingRange)
            {
                var algorithm = new Mock<PathfindingProcess>();
                algorithm.Setup(x => x.FindPath()).Returns(NullGraphPath.Instance);
                return algorithm.Object;
            }
        }

        private IMessenger messenger;
        private Mock<IRequestService<GraphVertexModel>> service;
        private readonly IGraph<GraphVertexModel> graph;

        public CreateRunButtonViewModelTests()
        {
            var random = new CongruentialRandom();
            var costLayer = new VertexCostLayer((9, 1), range => new VertexCost(random.NextInt(range), range));
            var obstacleLayer = new ObstacleLayer(random, 0);
            var neighborhoodLayer = new NeighborhoodLayer();
            var layers = new Layers(costLayer, obstacleLayer, neighborhoodLayer);
            var vertexFactory = new GraphVertexModelFactory();
            var graphFactory = new GraphFactory<GraphVertexModel>();
            var assemble = new GraphAssemble<GraphVertexModel>(vertexFactory, graphFactory);
            graph = assemble.AssembleGraph(layers, 25, 35);
        }

        [SetUp]
        public void SetUp()
        {
            service = new Mock<IRequestService<GraphVertexModel>>();
            IReadOnlyCollection<PathfindingRangeModel> pathfindingRange
                = new List<PathfindingRangeModel>()
            {
                new() { Order = 1, Position = new Coordinate(0, 0) },
                new() { Order = 2, Position = new Coordinate(3, 4) },
                new() { Order = 3, Position = new Coordinate(6, 7) },
                new() { Order = 4, Position = new Coordinate(12, 15) },
                new() { Order = 5, Position = new Coordinate(20, 20) }
            };
            service.Setup(x => x.ReadRangeAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(pathfindingRange));
            IReadOnlyCollection<AlgorithmRunHistoryModel> result =
                new AlgorithmRunHistoryModel().Enumerate().ToArray();
            service.Setup(x => x.CreateRunHistoriesAsync(
                    It.IsAny<IEnumerable<CreateAlgorithmRunHistoryRequest>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(result));
            messenger = new WeakReferenceMessenger();
        }

        [Test]
        public async Task CreateRunCommand_NoActivatedGraph_CantExecute()
        {
            var viewModel = new TestCreateRunButtonViewModel(service.Object,
                messenger, new NullLog());
            var canExecute = await viewModel.StartAlgorithmCommand.CanExecute.FirstOrDefaultAsync();

            Assert.That(canExecute, Is.False);
        }

        [Test]
        public async Task CreateRunCommand_HasActivatedGraph_ShouldExecute()
        {
            var viewModel = new TestCreateRunButtonViewModel(service.Object,
                messenger, new NullLog());
            bool isRunSent = false;
            void OnRunCreated(object recipient, RunCreatedMessaged msg)
            {
                isRunSent = true;
            }
            messenger.Register<RunCreatedMessaged>(this, OnRunCreated);

            messenger.Send(new GraphActivatedMessage(1, graph));
            await viewModel.StartAlgorithmCommand.Execute();

            Assert.Multiple(() =>
            {
                service.Verify(x => x.CreateRunHistoriesAsync(
                    It.IsAny<IEnumerable<CreateAlgorithmRunHistoryRequest>>(),
                    It.IsAny<CancellationToken>()), Times.Once);
                service.Verify(x => x.ReadRangeAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()), Times.Once);
                Assert.That(isRunSent);
            });
        }
    }
}
