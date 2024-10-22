using Autofac;
using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.TestUtils.Attributes;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Test
{
    [TestFixture, UnitTest]
    public class DeleteGraphButtonViewModelTests
    {
        [Test]
        public async Task DeleteCommand_SelectedGraphs_ShouldDelete()
        {
            using var mock = AutoMock.GetLoose();
            var ids = new[] { 1, 2 };
            var viewModel = mock.Create<GraphDeletionViewModel>();
            mock.Mock<IMessenger>().Setup(x => x.Send(
                It.IsAny<GraphSelectedMessage>(),
                It.IsAny<IsAnyToken>())).Callback<GraphSelectedMessage, object>((x, y) =>
                {
                    viewModel.GraphIds = new[] { 1, 2 };
                });
            mock.Mock<IRequestService<GraphVertexModel>>().Setup(x => x.DeleteGraphsAsync(
                   It.IsAny<int[]>(),
                   It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var messenger = mock.Container.Resolve<IMessenger>();
            messenger.Send(new GraphSelectedMessage(ids));

            await viewModel.DeleteCommand.Execute();

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>().Verify(x => x.DeleteGraphsAsync(
                    It.Is<int[]>(x=>x.SequenceEqual(ids)),
                    It.IsAny<CancellationToken>()), Times.Once);
                mock.Mock<IMessenger>().Verify(x => x.Send(
                    It.Is<GraphsDeletedMessage>(x => x.GraphIds.SequenceEqual(ids)),
                    It.IsAny<IsAnyToken>()), Times.Once);
            });
        }

        [Test]
        public async Task DeleteCommand_NoSelectedGraph_CantExecute()
        {
            using var mock = AutoMock.GetLoose();

            var viewModel = mock.Create<GraphDeletionViewModel>();

            var canExecute = await viewModel.DeleteCommand.CanExecute.FirstOrDefaultAsync();

            Assert.That(canExecute, Is.False);
        }
    }
}
