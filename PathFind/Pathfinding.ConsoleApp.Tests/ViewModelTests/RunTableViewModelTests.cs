using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using NUnit.Framework;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Tests.ViewModelTests
{
    [Category("Unit")]
    internal sealed class RunTableViewModelTests
    {
        [Test]
        public void OnRunCreated_ValidRun_ShouldAdd()
        {
            using var mock = AutoMock.GetLoose();

            var run = new RunStatisticsModel() { Id = 1 };
            mock.Mock<IMessenger>().Setup(x => x.Register(
                It.IsAny<object>(), 
                It.IsAny<IsAnyToken>(),
                It.IsAny<MessageHandler<object, RunCreatedMessaged>>()))
                .Callback<object, object, MessageHandler<object, RunCreatedMessaged>>((r, t, handler) => handler(r, new(run)));

            var viewModel = mock.Create<RunsTableViewModel>();

            Assert.That(viewModel.Runs.Count == 1);
        }

        [Test]
        public void OnGraphActivated_ValidGraph_ShouldGetRuns()
        {
            using var mock = AutoMock.GetLoose();

            var graph = new GraphModel<GraphVertexModel>() { Id = 1 };
            IReadOnlyCollection<RunStatisticsModel> runs = Enumerable.Range(1, 5)
                .Select(x => new RunStatisticsModel { Id = x })
                .ToList();
            
            mock.Mock<IMessenger>()
                .Setup(x => x.Register(
                    It.IsAny<object>(),
                    It.IsAny<IsAnyToken>(),
                    It.IsAny<MessageHandler<object, GraphActivatedMessage>>()))
                .Callback<object, object, MessageHandler<object, GraphActivatedMessage>>((r, t, handler) => handler(r, new(graph)));

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadStatisticsAsync(
                    It.Is<int>(x => x == 1), 
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(runs));

            var viewModel = mock.Create<RunsTableViewModel>();

            Assert.Multiple(() =>
            {
                Assert.That(viewModel.Runs.Count == runs.Count);
                Assert.That(viewModel.Runs.Count > 0);
            });
        }

        [Test]
        public async Task SelectRunCommand_ShouldSendMessage()
        {
            using var mock = AutoMock.GetLoose();

            var viewModel = mock.Create<RunsTableViewModel>();

            await viewModel.SelectRunsCommand.Execute(Array.Empty<RunInfoModel>());

            mock.Mock<IMessenger>()
                .Verify(x => x.Send(
                    It.IsAny<RunSelectedMessage>(),
                    It.IsAny<IsAnyToken>()), Times.Once);
        }

        [Test]
        public void DeleteGraphsMessage_ActivatedGraph_ShouldClearRuns()
        {
            using var mock = AutoMock.GetLoose();

            var graph = new GraphModel<GraphVertexModel>() { Id = 1 };
            IReadOnlyCollection<RunStatisticsModel> runs = Enumerable.Range(1, 5)
                .Select(x => new RunStatisticsModel { Id = x })
                .ToList();

            mock.Mock<IMessenger>()
                .Setup(x => x.Register(
                    It.IsAny<object>(),
                    It.IsAny<IsAnyToken>(),
                    It.IsAny<MessageHandler<object, GraphActivatedMessage>>()))
                .Callback<object, object, MessageHandler<object, GraphActivatedMessage>>((r, t, handler)
                => handler(r, new(graph)));

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadStatisticsAsync(
                    It.Is<int>(x => x == 1),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(runs));

            mock.Mock<IMessenger>()
                .Setup(x => x.Register(
                    It.IsAny<object>(),
                    It.IsAny<IsAnyToken>(),
                    It.IsAny<MessageHandler<object, GraphsDeletedMessage>>()))
                .Callback<object, object, MessageHandler<object, GraphsDeletedMessage>>((r, t, handler) => 
                    handler(r, new(new[] { 1 })));

            var viewModel = mock.Create<RunsTableViewModel>();

            Assert.Multiple(() =>
            {
                mock.Mock<IMessenger>()
                    .Verify(x => x.Register(
                        It.IsAny<object>(),
                        It.IsAny<IsAnyToken>(),
                        It.IsAny<MessageHandler<object, GraphActivatedMessage>>()), Times.Once);

                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadStatisticsAsync(
                        It.Is<int>(x => x == 1),
                        It.IsAny<CancellationToken>()), Times.Once);

                mock.Mock<IMessenger>()
                    .Verify(x => x.Register(
                        It.IsAny<object>(),
                        It.IsAny<IsAnyToken>(),
                        It.IsAny<MessageHandler<object, GraphsDeletedMessage>>()), Times.Once);

                Assert.That(viewModel.Runs.Count == 0);
            });
        }

        [Test]
        public void DeleteRunsMessage_SendAllRuns_ShouldDeleteAllAndSendFalseReadOnlyMessage()
        {
            using var mock = AutoMock.GetLoose();

            var graph = new GraphModel<GraphVertexModel>() { Id = 1 };
            IReadOnlyCollection<RunStatisticsModel> runs = Enumerable.Range(1, 5)
                .Select(x => new RunStatisticsModel { Id = x })
                .ToList();

            mock.Mock<IMessenger>()
                .Setup(x => x.Register(
                    It.IsAny<object>(),
                    It.IsAny<IsAnyToken>(),
                    It.IsAny<MessageHandler<object, GraphActivatedMessage>>()))
                .Callback<object, object, MessageHandler<object, GraphActivatedMessage>>((r, t, handler)
                => handler(r, new(graph)));

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadStatisticsAsync(
                    It.Is<int>(x => x == 1),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(runs));

            mock.Mock<IMessenger>()
                .Setup(x => x.Register(
                    It.IsAny<object>(),
                    It.IsAny<IsAnyToken>(),
                    It.IsAny<MessageHandler<object, RunsDeletedMessage>>()))
                .Callback<object, object, MessageHandler<object, RunsDeletedMessage>>((r, t, handler)
                => handler(r, new(runs.Select(x => x.Id).ToArray())));

            var viewModel = mock.Create<RunsTableViewModel>();

            Assert.Multiple(() =>
            {
                mock.Mock<IMessenger>()
                    .Verify(x => x.Register(
                        It.IsAny<object>(),
                        It.IsAny<IsAnyToken>(),
                        It.IsAny<MessageHandler<object, GraphActivatedMessage>>()), Times.Once);

                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadStatisticsAsync(
                        It.Is<int>(x => x == 1),
                        It.IsAny<CancellationToken>()), Times.Once);

                mock.Mock<IMessenger>()
                    .Verify(x => x.Register(
                        It.IsAny<object>(),
                        It.IsAny<IsAnyToken>(),
                        It.IsAny<MessageHandler<object, RunsDeletedMessage>>()), Times.Once);

                mock.Mock<IMessenger>()
                    .Verify(x => x.Send(
                        It.Is<GraphBecameReadOnlyMessage>(x => x.Id == 1 && x.Became == false),
                        It.IsAny<IsAnyToken>()), Times.Once);

                Assert.That(viewModel.Runs.Count == 0);
            });
        }
    }
}
