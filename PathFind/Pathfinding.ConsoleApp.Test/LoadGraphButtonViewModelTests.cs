using Autofac;
using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Shared.Extensions;
using Pathfinding.TestUtils.Attributes;
using System.Collections;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Test
{
    [TestFixture, UnitTest]
    internal class LoadGraphButtonViewModelTests
    {
        [TestCaseSource(nameof(LoadGraphData))]
        public async Task LoadGraph_ShouldLoad(string filePath, Times methodCalls, bool shouldLoad)
        {
            using (var mock = AutoMock.GetLoose(x =>
            {
                x.RegisterInstance(new Func<string, Stream>(x => Stream.Null))
                    .AsSelf().SingleInstance();
            }))
            {
                var history = new PathfindingHistoryModel<GraphVertexModel>()
                {
                    Graph = ModelBuilder.CreateEmptyModel()
                }.Enumerate().ToArray();
                IReadOnlyCollection<PathfindingHistoryModel<GraphVertexModel>> histories = history;
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Setup(x => x.CreatePathfindingHistoriesAsync(
                        It.IsAny<IEnumerable<PathfindingHistorySerializationModel>>(),
                        It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(histories));

                var viewModel = mock.Create<GraphImportViewModel>();
                viewModel.FilePath = filePath;
                bool canExecute = await viewModel.LoadGraphCommand.CanExecute.FirstOrDefaultAsync();

                if (canExecute)
                {
                    await viewModel.LoadGraphCommand.Execute();
                }

                Assert.Multiple(() =>
                {
                    Assert.That(canExecute == shouldLoad);
                    mock.Mock<IRequestService<GraphVertexModel>>()
                        .Verify(x => x.CreatePathfindingHistoriesAsync(
                            It.IsAny<IEnumerable<PathfindingHistorySerializationModel>>(),
                            It.IsAny<CancellationToken>()), methodCalls);
                    mock.Mock<ISerializer<IEnumerable<PathfindingHistorySerializationModel>>>()
                        .Verify(x => x.DeserializeFromAsync(
                            It.IsAny<Stream>(),
                            It.IsAny<CancellationToken>()), methodCalls);
                    mock.Mock<IMessenger>()
                        .Verify(x => x.Send(
                            It.IsAny<GraphCreatedMessage>(),
                            It.IsAny<IsAnyToken>()), methodCalls);
                });
            }
        }

        private static IEnumerable LoadGraphData
        {
            get
            {
                yield return new TestCaseData("Test", Times.Once(), true);
                yield return new TestCaseData(string.Empty, Times.Never(), false);
            }
        }
    }
}
