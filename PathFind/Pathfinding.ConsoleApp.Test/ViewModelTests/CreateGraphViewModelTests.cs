using CommunityToolkit.Mvvm.Messaging;
using Moq;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Logging.Loggers;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.TestUtils.Attributes;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Test.ViewModelTests
{
    [TestFixture, UnitTest]
    public class CreateGraphViewModelTests
    {
        private readonly Mock<IRequestService<VertexModel>> service;
        private readonly Mock<IGraphAssemble<VertexModel>> graphAssemble;
        private readonly IMessenger messenger;

        public CreateGraphViewModelTests()
        {
            service = new Mock<IRequestService<VertexModel>>();
            graphAssemble = new Mock<IGraphAssemble<VertexModel>>();
            graphAssemble.Setup(x => x.AssembleGraph(It.IsAny<IReadOnlyList<int>>()))
               .Returns(Graph<VertexModel>.Empty);
            messenger = new WeakReferenceMessenger();
        }

        [TearDown]
        public void CleanUp()
        {
            messenger.UnregisterAll(this);
        }

        [Test]
        public async Task CreateGraphCommand_FullData_ShouldCreate()
        {           
            GraphCreatedMessage? message = null;
            messenger.Register<GraphCreatedMessage>(this, (r, msg) =>
            {
                message = msg;
            });
            var viewModel = new CreateGraphViewModel(service.Object,
                graphAssemble.Object, messenger, new NullLog());
            viewModel.NeighborhoodFactory = new MooreNeighborhoodFactory();
            viewModel.Name = "Test";
            viewModel.Length = 1;
            viewModel.SmoothLevel = 0;
            viewModel.Width = 1;
            viewModel.Obstacles = 1;

            var canExecute = await viewModel.CreateCommand.CanExecute.FirstOrDefaultAsync();
            await viewModel.CreateCommand.Execute();

            Assert.Multiple(() =>
            {
                service.Verify(service => service.CreateGraphAsync(It.IsAny<CreateGraphRequest<VertexModel>>(),
                    It.IsAny<CancellationToken>()), Times.Once);
                Assert.That(message, Is.Not.Null);
                Assert.That(canExecute, Is.True);
            });
        }

        [Test]
        public async Task CanExecute_ValidData_ShouldReturnTrue()
        {
            var viewModel = new CreateGraphViewModel(service.Object,
                graphAssemble.Object, messenger, new NullLog());

            viewModel.NeighborhoodFactory = new MooreNeighborhoodFactory();
            viewModel.Name = "Test";
            viewModel.Length = 1;
            viewModel.SmoothLevel = 0;
            viewModel.Width = 1;
            viewModel.Obstacles = 1;

            var canExecute = await viewModel.CreateCommand.CanExecute.FirstOrDefaultAsync();

            Assert.That(canExecute, Is.True);
        }

        [Test]
        public async Task CanExecute_DataIsUnset_ShouldReturnFalse()
        {
            var viewModel = new CreateGraphViewModel(service.Object,
                graphAssemble.Object, messenger, new NullLog());

            var canExecute = await viewModel.CreateCommand.CanExecute.FirstOrDefaultAsync();

            Assert.That(canExecute, Is.False);
        }
    }
}
