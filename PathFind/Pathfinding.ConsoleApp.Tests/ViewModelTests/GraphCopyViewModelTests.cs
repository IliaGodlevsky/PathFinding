using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using NUnit.Framework;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Tests.ViewModelTests
{
    [Category("Unit")]
    internal class GraphCopyViewModelTests
    {
        [Test]
        public async Task CopyCommand_CanExecute_ShouldCopy()
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
            IReadOnlyCollection<PathfindingHistoryModel<GraphVertexModel>> result
                = Enumerable.Range(1, 5)
                .Select(x => new PathfindingHistoryModel<GraphVertexModel>()
                {
                    Graph = new()
                    {
                        Id = x,
                        Vertices = Array.Empty<GraphVertexModel>(),
                        DimensionSizes = Array.Empty<int>(),
                        Name = string.Empty
                    }
                }).ToList();

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadSerializationHistoriesAsync(
                    It.IsAny<IEnumerable<int>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(histories));

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.CreatePathfindingHistoriesAsync(
                    It.IsAny<IEnumerable<PathfindingHistorySerializationModel>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(result));

            mock.Mock<IMessenger>().Setup(x => x.Register(
                    It.IsAny<object>(),
                    It.IsAny<IsAnyToken>(),
                    It.IsAny<MessageHandler<object, GraphSelectedMessage>>()))
               .Callback<object, object, MessageHandler<object, GraphSelectedMessage>>((r, t, handler)
                    => handler(r, new GraphSelectedMessage(models)));

            var viewModel = mock.Create<GraphCopyViewModel>();

            var command = viewModel.CopyGraphCommand;

            if (await command.CanExecute.FirstOrDefaultAsync())
            {
                await command.Execute();
            }

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadSerializationHistoriesAsync(
                        It.IsAny<IEnumerable<int>>(),
                        It.IsAny<CancellationToken>()), Times.Once);

                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.CreatePathfindingHistoriesAsync(
                        It.IsAny<IEnumerable<PathfindingHistorySerializationModel>>(),
                        It.IsAny<CancellationToken>()), Times.Once);

                mock.Mock<IMessenger>()
                    .Verify(x => x.Send(
                        It.IsAny<GraphCreatedMessage>(),
                        It.IsAny<IsAnyToken>()), Times.Once);
            });
        }

        [Test]
        public async Task CopyGraphCommand_ThrowsException_ShouldLogError()
        {
            using var mock = AutoMock.GetLoose();

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadSerializationHistoriesAsync(
                    It.IsAny<IEnumerable<int>>(),
                    It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var viewModel = mock.Create<GraphCopyViewModel>();

            await viewModel.CopyGraphCommand.Execute();

            mock.Mock<ILog>()
                .Verify(x => x.Error(
                    It.IsAny<Exception>(),
                    It.IsAny<string>()), Times.Once);
        }
    }
}
