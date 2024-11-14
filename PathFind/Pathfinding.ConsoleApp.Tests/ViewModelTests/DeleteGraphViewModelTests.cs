using Autofac.Extras.Moq;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using NUnit.Framework;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Service.Interface;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Tests.ViewModelTests
{
    [Category("Unit")]
    internal sealed class DeleteGraphViewModelTests
    {
        [Test]
        public async Task DeleteGraphCommand_MoreThanOneGraph_ShouldExecute()
        {
            using var mock = AutoMock.GetLoose();
            GraphInfoModel[] models = new[]
            {
                new GraphInfoModel() { Id = 1 },
                new GraphInfoModel() { Id = 2 },
                new GraphInfoModel() { Id = 3 }
            };

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.DeleteGraphsAsync(
                    It.Is<IEnumerable<int>>(x => x.SequenceEqual(models.Select(x => x.Id))),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            mock.Mock<IMessenger>().Setup(x => x.Register(
                    It.IsAny<object>(),
                    It.IsAny<IsAnyToken>(),
                    It.IsAny<MessageHandler<object, GraphSelectedMessage>>()))
               .Callback<object, object, MessageHandler<object, GraphSelectedMessage>>((r, t, handler) 
                    => handler(r, new GraphSelectedMessage(models)));

            var viewModel = mock.Create<GraphDeleteViewModel>();

            var command = viewModel.DeleteCommand;
            var canExecute = await viewModel.DeleteCommand.CanExecute.FirstOrDefaultAsync();
            if (canExecute)
            {
                await command.Execute();
            }

            Assert.Multiple(() =>
            {
                Assert.That(canExecute, Is.True);
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.DeleteGraphsAsync(
                        It.Is<IEnumerable<int>>(x => x.SequenceEqual(models.Select(x => x.Id))),
                        It.IsAny<CancellationToken>()), Times.Once);
                mock.Mock<IMessenger>()
                    .Verify(x => x.Register(
                        It.IsAny<GraphDeleteViewModel>(),
                        It.IsAny<IsAnyToken>(),
                        It.IsAny<MessageHandler<object, GraphSelectedMessage>>()), Times.Once);
                mock.Mock<IMessenger>()
                    .Verify(x => x.Send(
                        It.Is<GraphsDeletedMessage>(x => models.Select(x => x.Id).SequenceEqual(x.GraphIds)),
                        It.IsAny<IsAnyToken>()), Times.Once);
            });
        }

        [Test]
        public async Task DeleteGraphCommand_NoGraphs_ShouldNotExecute()
        {
            using var mock = AutoMock.GetLoose();
            var models = Array.Empty<GraphInfoModel>();

            var viewModel = mock.Create<GraphDeleteViewModel>();

            var command = viewModel.DeleteCommand;
            var canExecute = await viewModel.DeleteCommand.CanExecute.FirstOrDefaultAsync();
            if (canExecute)
            {
                await command.Execute();
            }

            Assert.Multiple(() =>
            {
                Assert.That(canExecute, Is.False);
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.DeleteGraphsAsync(
                        It.IsAny<IEnumerable<int>>(),
                        It.IsAny<CancellationToken>()), Times.Never);
                mock.Mock<IMessenger>()
                    .Verify(x => x.Send(
                        It.IsAny<GraphsDeletedMessage>(),
                        It.IsAny<IsAnyToken>()), Times.Never);
            });
        }
    }
}
