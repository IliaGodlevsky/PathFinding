using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using NUnit.Framework;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Tests.ViewModelTests
{
    [Category("Unit")]
    internal sealed class GraphTableViewModelTests
    {
        [Test]
        public async Task LoadGraphsCommand_ShouldAddGraphs()
        {
            using var mock = AutoMock.GetLoose();

            IReadOnlyCollection<GraphInformationModel> graphs =
                Enumerable.Range(1, 5).Select(x => new GraphInformationModel
                {
                    Id = x,
                    Dimensions = Array.Empty<int>()
                }).ToList();
            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadAllGraphInfoAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(graphs));

            var viewModel = mock.Create<GraphTableViewModel>();

            await viewModel.LoadGraphsCommand.Execute();

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadAllGraphInfoAsync(
                        It.IsAny<CancellationToken>()), Times.Once);
                Assert.That(viewModel.Graphs.Count == graphs.Count);
            });
        }

        [Test]
        public async Task LoadGraphsCommand_ThrowsException_ShouldLogError()
        {
            using var mock = AutoMock.GetLoose();

            mock.Mock<IRequestService<GraphVertexModel>>()
                 .Setup(x => x.ReadAllGraphInfoAsync(It.IsAny<CancellationToken>()))
                 .Throws(new Exception());

            var viewModel = mock.Create<GraphTableViewModel>();

            await viewModel.LoadGraphsCommand.Execute();

            mock.Mock<ILog>()
                .Verify(x => x.Error(
                    It.IsAny<Exception>(),
                    It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ActvateGraphCommand_ShouldSendMessage()
        {
            using var mock = AutoMock.GetLoose();

            var graph = new GraphModel<GraphVertexModel>()
            {
                Id = 1,
                Name = "Test",
                Vertices = Array.Empty<GraphVertexModel>(),
                DimensionSizes = Array.Empty<int>()
            };

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadGraphAsync(
                    It.Is<int>(x => x == 1),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(graph));
            mock.Mock<IMessenger>()
                .Setup(x => x.Send(It.IsAny<AsyncGraphActivatedMessage>(),
                    It.IsAny<IsAnyToken>()))
                .Callback<AsyncGraphActivatedMessage, object>((m, t) => m.Signal(default(System.Reactive.Unit)));

            var viewModel = mock.Create<GraphTableViewModel>();

            await viewModel.ActivateGraphCommand.Execute(1);

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadGraphAsync(
                        It.Is<int>(x => x == 1),
                        It.IsAny<CancellationToken>()), Times.Once);
                mock.Mock<IMessenger>()
                    .Verify(x => x.Send(
                        It.IsAny<AsyncGraphActivatedMessage>(),
                        It.IsAny<int>()), Times.Exactly(2));
                mock.Mock<IMessenger>()
                    .Verify(x => x.Send(
                        It.IsAny<GraphActivatedMessage>(),
                        It.IsAny<IsAnyToken>()), Times.Exactly(2));
            });
        }

        [Test]
        public async Task ActivateGraphCommand_ThrowException_ShouldLogError()
        {
            using var mock = AutoMock.GetLoose();

            mock.Mock<IRequestService<GraphVertexModel>>()
               .Setup(x => x.ReadGraphAsync(
                   It.IsAny<int>(),
                   It.IsAny<CancellationToken>()))
               .Throws(new Exception());

            var viewModel = mock.Create<GraphTableViewModel>();

            await viewModel.ActivateGraphCommand.Execute(new());

            mock.Mock<ILog>()
                .Verify(x => x.Error(
                    It.IsAny<Exception>(),
                    It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task SelectedGraphsCommand_ShouldSendMessage()
        {
            using var mock = AutoMock.GetLoose();

            var models = Enumerable.Range(1, 5).ToArray();

            var viewModel = mock.Create<GraphTableViewModel>();

            await viewModel.SelectGraphsCommand.Execute(models);

            mock.Mock<IMessenger>()
                .Verify(x => x.Send(
                    It.IsAny<GraphSelectedMessage>(),
                    It.IsAny<IsAnyToken>()), Times.Once);
        }

        //[Test]
        //public async Task OnObstaclesChanged_ShouldChangeObstaclesCount()
        //{
        //    using var mock = AutoMock.GetLoose(RegisterSmoothLevels);
        //}
    }
}
