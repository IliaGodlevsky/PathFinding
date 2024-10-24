using Autofac;
using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
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
                var algorithm = new Mock<PathfindingProcess>(pathfindingRange);
                algorithm.Setup(x => x.FindPath()).Returns(NullGraphPath.Instance);
                return algorithm.Object;
            }
        }

        [Test]
        public async Task CreateRunCommand_NoActivatedGraph_CantExecute()
        {
            using var mock = AutoMock.GetLoose();
            var viewModel = mock.Create<TestCreateRunButtonViewModel>();
            var canExecute = await viewModel.StartAlgorithmCommand.CanExecute.FirstOrDefaultAsync();

            Assert.That(canExecute, Is.False);
        }

        [Test]
        public async Task StartAlgorithmCommand_HasActivatedGraph_ShouldExecute()
        {
            using var mock = AutoMock.GetLoose();

            IReadOnlyCollection<AlgorithmRunHistoryModel> result =
                new AlgorithmRunHistoryModel().Enumerate().ToArray();
            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.CreateRunHistoriesAsync(
                    It.IsAny<IEnumerable<CreateAlgorithmRunHistoryRequest>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(result));

            mock.Mock<IMessenger>().Setup(x => x.Send(
                It.IsAny<QueryPathfindingRangeMessage>(),
                It.IsAny<IsAnyToken>())).Callback<QueryPathfindingRangeMessage, object>((x,y) =>
                {
                    var source = new GraphVertexModel(Coordinate.Empty);
                    var target = new GraphVertexModel(Coordinate.Empty);
                    x.PathfindingRange = new[] { source, target };
                });

            var viewModel = mock.Create<TestCreateRunButtonViewModel>();

            mock.Mock<IMessenger>().Setup(x => x.Send(
                It.IsAny<GraphActivatedMessage>(),
                It.IsAny<IsAnyToken>())).Callback<GraphActivatedMessage, object>((x, y) =>
                {
                    viewModel.Graph = x.Graph;
                    viewModel.GraphId = x.GraphId;
                });

            var messenger = mock.Container.Resolve<IMessenger>();
            messenger.Send(new GraphActivatedMessage(1, Graph<GraphVertexModel>.Empty));
            await viewModel.StartAlgorithmCommand.Execute();

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                .Verify(x => x.CreateRunHistoriesAsync(
                    It.IsAny<IEnumerable<CreateAlgorithmRunHistoryRequest>>(),
                    It.IsAny<CancellationToken>()), Times.Once);
                mock.Mock<IMessenger>().Verify(x => x.Send(
                    It.IsAny<RunCreatedMessaged>(),
                    It.IsAny<IsAnyToken>()), Times.Once);
            });
        }
    }
}
