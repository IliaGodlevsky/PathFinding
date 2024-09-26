using CommunityToolkit.Mvvm.Messaging;
using Moq;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Logging.Loggers;
using Pathfinding.Service.Interface;
using Pathfinding.TestUtils.Attributes;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Test
{
    [TestFixture, UnitTest]
    public class DeleteGraphButtonViewModelTests
    {
        private Mock<IRequestService<VertexModel>> service;
        private IMessenger messenger;

        [SetUp]
        public void SetUp()
        {
            messenger = new WeakReferenceMessenger();
            service = new Mock<IRequestService<VertexModel>>();
            service.Setup(x => x.DeleteGraphsAsync(
                It.IsAny<int[]>(),
                It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
        }

        [Test]
        public async Task DeleteCommand_SelectedGraphs_ShouldDelete()
        {
            var viewModel = new DeleteGraphButtonViewModel(messenger,
                service.Object, new NullLog());
            int[] ids = new[] { 1, 2 };
            bool isDeleted = false;
            void OnDeleted(object recipient, GraphsDeletedMessage msg)
            {
                isDeleted = msg.GraphIds.SequenceEqual(ids);
            }
            messenger.Register<GraphsDeletedMessage>(this, OnDeleted);
            messenger.Send(new GraphSelectedMessage(ids));

            await viewModel.DeleteCommand.Execute();

            Assert.Multiple(() =>
            {
                service.Verify(x => x.DeleteGraphsAsync(
                    It.IsAny<int[]>(),
                    It.IsAny<CancellationToken>()), Times.Once);
                Assert.That(isDeleted);
            });
        }
    }
}
