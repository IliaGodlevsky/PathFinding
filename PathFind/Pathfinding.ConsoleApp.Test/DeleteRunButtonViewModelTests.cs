using Autofac;
using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Service.Interface;
using Pathfinding.TestUtils.Attributes;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Test
{
    [TestFixture, UnitTest]
    internal class DeleteRunButtonViewModelTests
    {
        [Test]
        public async Task DeleteCommand_SelectedRuns_ShouldDelete()
        {
            using var mock = AutoMock.GetLoose();
            var ids = new[] { 1, 2 };
            var viewModel = mock.Create<DeleteRunButtonViewModel>();
            mock.Mock<IMessenger>().Setup(x => x.Send(
                It.Is<RunSelectedMessage>(x => x.SelectedRuns.SequenceEqual(ids)),
                It.IsAny<IsAnyToken>())).Callback<RunSelectedMessage, object>((x, y) =>
                {
                    viewModel.RunsIds = ids;
                });
            mock.Mock<IRequestService<GraphVertexModel>>().Setup(x => x.DeleteRunsAsync(
                It.Is<int[]>(x => x.SequenceEqual(ids)),
                It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            var messenger = mock.Container.Resolve<IMessenger>();
            messenger.Send(new RunSelectedMessage(ids));

            await viewModel.DeleteRunCommand.Execute();

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.DeleteRunsAsync(
                        It.Is<int[]>(x => x.SequenceEqual(ids)),
                        It.IsAny<CancellationToken>()), Times.Once);
                mock.Mock<IMessenger>().Verify(x => x.Send(
                    It.Is<RunsDeletedMessage>(x => x.RunIds.SequenceEqual(ids)),
                    It.IsAny<IsAnyToken>()), Times.Once);
            });
        }
    }
}
