using Autofac;
using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Service.Interface;
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
                IReadOnlyCollection<PathfindingHistoryModel<VertexModel>> histories
                    = new PathfindingHistoryModel<VertexModel>().Enumerate().ToArray();
                mock.Mock<IRequestService<VertexModel>>()
                    .Setup(x => x.CreatePathfindingHistoriesAsync(
                        It.IsAny<IEnumerable<PathfindingHistorySerializationModel>>(),
                        It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(histories));

                var viewModel = mock.Create<LoadGraphButtonViewModel>();
                viewModel.FilePath = filePath;

                await viewModel.LoadGraphCommand.Execute();

                Assert.Multiple(() =>
                {
                    mock.Mock<IRequestService<VertexModel>>()
                        .Verify(x => x.CreatePathfindingHistoriesAsync(
                            It.IsAny<IEnumerable<PathfindingHistorySerializationModel>>(),
                            It.IsAny<CancellationToken>()), methodCalls);
                    mock.Mock<ISerializer<List<PathfindingHistorySerializationModel>>>()
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
