using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using NUnit.Framework;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Serialization;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Tests.ViewModelTests
{
    [Category("Unit")]
    internal class GraphExportViewModelTests
    {
        [Test]
        public async Task ExportGraphCommand_HasGraphs_ShouldExport()
        {
            using var mock = AutoMock.GetLoose();

            GraphInfoModel[] models = new[]
            {
                new GraphInfoModel() { Id = 1 },
                new GraphInfoModel() { Id = 2 },
                new GraphInfoModel() { Id = 3 }
            };

            IReadOnlyCollection<PathfindingHistorySerializationModel> histories
               = Enumerable.Range(1, 5)
               .Select(x => new PathfindingHistorySerializationModel())
               .ToArray();

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadSerializationHistoriesAsync(
                    It.IsAny<IEnumerable<int>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(histories));

            mock.Mock<IMessenger>().Setup(x => x.Register(
                    It.IsAny<object>(),
                    It.IsAny<IsAnyToken>(),
                    It.IsAny<MessageHandler<object, GraphSelectedMessage>>()))
               .Callback<object, object, MessageHandler<object, GraphSelectedMessage>>((r, t, handler)
                    => handler(r, new GraphSelectedMessage(models)));

            var viewModel = mock.Create<GraphExportViewModel>();

            var command = viewModel.ExportGraphCommand;
            if (await command.CanExecute.FirstOrDefaultAsync())
            {
                await command.Execute(() => new MemoryStream());
            }

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadSerializationHistoriesAsync(
                        It.IsAny<IEnumerable<int>>(),
                        It.IsAny<CancellationToken>()), Times.Once);

                mock.Mock<IMessenger>()
                    .Verify(x => x.Register(
                        It.IsAny<object>(),
                        It.IsAny<IsAnyToken>(),
                        It.IsAny<MessageHandler<object, GraphSelectedMessage>>()), Times.Once);

                mock.Mock<ISerializer<IEnumerable<PathfindingHistorySerializationModel>>>()
                    .Verify(x => x.SerializeToAsync(
                        It.IsAny<IEnumerable<PathfindingHistorySerializationModel>>(),
                        It.IsAny<Stream>(),
                        It.IsAny<CancellationToken>()), Times.Once);
            });
        }
    }
}
