using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using NUnit.Framework;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Shared.Extensions;
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

            PathfindingHisotiriesSerializationModel histories
               = Enumerable.Range(1, 5)
               .Select(x => new PathfindingHistorySerializationModel())
               .ToArray()
               .To(x => new PathfindingHisotiriesSerializationModel() { Histories = x.ToList() });

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
            viewModel.Options = ExportOptions.WithRuns;

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

                mock.Mock<ISerializer<PathfindingHisotiriesSerializationModel>>()
                    .Verify(x => x.SerializeToAsync(
                        It.IsAny<PathfindingHisotiriesSerializationModel>(),
                        It.IsAny<Stream>(),
                        It.IsAny<CancellationToken>()), Times.Once);
            });
        }

        [Test]
        public async Task ExportGraphCommand_NullStream_ShouldNotExport()
        {
            using var mock = AutoMock.GetLoose();

            var viewModel = mock.Create<GraphExportViewModel>();

            var command = viewModel.ExportGraphCommand;
            if (await command.CanExecute.FirstOrDefaultAsync())
            {
                await command.Execute(() => Stream.Null);
            }

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadSerializationHistoriesAsync(
                        It.IsAny<IEnumerable<int>>(),
                        It.IsAny<CancellationToken>()), Times.Never);

                mock.Mock<ISerializer<IEnumerable<PathfindingHistorySerializationModel>>>()
                    .Verify(x => x.SerializeToAsync(
                        It.IsAny<IEnumerable<PathfindingHistorySerializationModel>>(),
                        It.IsAny<Stream>(),
                        It.IsAny<CancellationToken>()), Times.Never);
            });
        }

        [Test]
        public async Task ExportGraphCommand_ThrowsException_ShouldLogError()
        {
            using var mock = AutoMock.GetLoose();

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadSerializationHistoriesAsync(
                    It.IsAny<IEnumerable<int>>(),
                    It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var viewModel = mock.Create<GraphExportViewModel>();

            var command = viewModel.ExportGraphCommand;
            await command.Execute(() => new MemoryStream());

            mock.Mock<ILog>()
                .Verify(x => x.Error(
                    It.IsAny<Exception>(),
                    It.IsAny<string>()), Times.Once);
        }
    }
}
