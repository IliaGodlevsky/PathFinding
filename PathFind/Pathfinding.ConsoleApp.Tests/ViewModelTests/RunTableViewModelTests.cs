﻿using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using NUnit.Framework;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Core;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Shared.Extensions;
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

            var run = new RunStatisticsModel() { Id = 1 }.Enumerate().ToArray();
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
                    It.IsAny<MessageHandler<object, AsyncGraphActivatedMessage>>()))
                .Callback<object, object, MessageHandler<object, AsyncGraphActivatedMessage>>((r, t, handler) => handler(r, new(graph)));

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

            await viewModel.SelectRunsCommand.Execute(Array.Empty<int>());

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
                    It.IsAny<MessageHandler<object, AsyncGraphActivatedMessage>>()))
                .Callback<object, object, MessageHandler<object, AsyncGraphActivatedMessage>>((r, t, handler)
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
                        It.IsAny<MessageHandler<object, AsyncGraphActivatedMessage>>()), Times.Once);

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
                    It.IsAny<MessageHandler<object, AsyncGraphActivatedMessage>>()))
                .Callback<object, object, MessageHandler<object, AsyncGraphActivatedMessage>>((r, t, handler)
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
                    It.IsAny<MessageHandler<object, AsyncRunsDeletedMessage>>()))
                .Callback<object, object, MessageHandler<object, AsyncRunsDeletedMessage>>((r, t, handler)
                => handler(r, new(runs.Select(x => x.Id).ToArray())));

            var viewModel = mock.Create<RunsTableViewModel>();

            Assert.Multiple(() =>
            {
                mock.Mock<IMessenger>()
                    .Verify(x => x.Register(
                        It.IsAny<object>(),
                        It.IsAny<IsAnyToken>(),
                        It.IsAny<MessageHandler<object, AsyncGraphActivatedMessage>>()), Times.Once);

                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadStatisticsAsync(
                        It.Is<int>(x => x == 1),
                        It.IsAny<CancellationToken>()), Times.Once);

                mock.Mock<IMessenger>()
                    .Verify(x => x.Register(
                        It.IsAny<object>(),
                        It.IsAny<IsAnyToken>(),
                        It.IsAny<MessageHandler<object, AsyncRunsDeletedMessage>>()), Times.Once);

                mock.Mock<IMessenger>()
                    .Verify(x => x.Send(
                        It.Is<GraphStateChangedMessage>(x => x.Id == 1 && x.Status == GraphStatuses.Editable),
                        It.IsAny<IsAnyToken>()), Times.Once);

                Assert.That(viewModel.Runs.Count == 0);
            });
        }

        [Test]
        public void ActivateGraph_ThrowsException_ShouldLogError()
        {
            using var mock = AutoMock.GetLoose();

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadStatisticsAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            mock.Mock<IMessenger>()
                .Setup(x => x.Register(
                    It.IsAny<object>(),
                    It.IsAny<IsAnyToken>(),
                    It.IsAny<MessageHandler<object, AsyncGraphActivatedMessage>>()))
                .Callback<object, object, MessageHandler<object, AsyncGraphActivatedMessage>>((r, t, handler)
                => handler(r, new(new())));

            var viewModel = mock.Create<RunsTableViewModel>();

            mock.Mock<ILog>()
                .Verify(x => x.Error(
                    It.IsAny<Exception>(),
                    It.IsAny<string>()), Times.Once);
        }
    }
}
