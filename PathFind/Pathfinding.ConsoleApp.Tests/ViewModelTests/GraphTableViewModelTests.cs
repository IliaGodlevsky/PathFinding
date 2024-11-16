using Autofac;
using Autofac.Extras.Moq;
using Autofac.Features.AttributeFilters;
using Autofac.Features.Metadata;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using NUnit.Framework;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.Tests.ViewModelTests
{
    [Category("Unit")]
    internal sealed class GraphTableViewModelTests
    {
        [Test]
        public async Task LoadGraphsCommand_ShouldAddGraphs()
        {
            using var mock = AutoMock.GetLoose(RegisterSmoothLevels);

            IReadOnlyCollection<GraphInformationModel> graphs =
                Enumerable.Range(1, 5).Select(x => new GraphInformationModel 
                { 
                    Id = x, 
                    Dimensions = Array.Empty<int>()
                }).ToList();
            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadAllGraphInfoAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(graphs));

            var viewModel = mock.Create<GraphTableViewModel>();

            await viewModel.LoadGraphsCommand.Execute();

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadAllGraphInfoAsync(
                        It.IsAny<CancellationToken>()), Times.Once);
                Assert.That(viewModel.Graphs.Count == graphs.Count);
            });
        }

        [Test]
        public async Task LoadGraphsCommand_ThrowsException_ShouldLogError()
        {
            using var mock = AutoMock.GetLoose(RegisterSmoothLevels);

            mock.Mock<IRequestService<GraphVertexModel>>()
                 .Setup(x => x.ReadAllGraphInfoAsync(It.IsAny<CancellationToken>()))
                 .Callback<CancellationToken>(x => throw new Exception());

            var viewModel = mock.Create<GraphTableViewModel>();

            await viewModel.LoadGraphsCommand.Execute();

            mock.Mock<ILog>()
                .Verify(x => x.Error(
                    It.IsAny<Exception>(),
                    It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ActvateGraphCommand_ShouldSendMessage()
        {
            using var mock = AutoMock.GetLoose(RegisterSmoothLevels);

            var graph = new GraphModel<GraphVertexModel>()
            {
                Id = 1,
                Name = "Test",
                Neighborhood = NeighborhoodNames.Moore,
                SmoothLevel = SmoothLevelNames.No,
                Graph = Graph<GraphVertexModel>.Empty,
                IsReadOnly = false
            };

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadGraphAsync(
                    It.Is<int>(x => x == 1),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(graph));

            var info = new GraphInfoModel() { Id = 1 };

            var viewModel = mock.Create<GraphTableViewModel>();

            await viewModel.ActivateGraphCommand.Execute(info);

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadGraphAsync(
                        It.Is<int>(x => x == 1),
                        It.IsAny<CancellationToken>()), Times.Once);
                mock.Mock<IMessenger>()
                    .Verify(x => x.Send(
                        It.IsAny<GraphActivatedMessage>(), 
                        It.IsAny<int>()), Times.Exactly(2));
                mock.Mock<IMessenger>()
                    .Verify(x => x.Send(
                        It.IsAny<GraphActivatedMessage>(),
                        It.IsAny<IsAnyToken>()), Times.Exactly(3));
            });
        }

        [Test]
        public async Task ActivateGraphCommand_ThrowException_ShouldLogError()
        {
            using var mock = AutoMock.GetLoose(RegisterSmoothLevels);

            mock.Mock<IRequestService<GraphVertexModel>>()
               .Setup(x => x.ReadGraphAsync(
                   It.IsAny<int>(),
                   It.IsAny<CancellationToken>()))
               .Returns<int, CancellationToken>((x, t) => throw new Exception());

            var viewModel = mock.Create<GraphTableViewModel>();

            await viewModel.ActivateGraphCommand.Execute(new());

            mock.Mock<ILog>()
                .Verify(x => x.Error(
                    It.IsAny<Exception>(),
                    It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task SelectedGraphsCommand_ShouldSendMessage()
        {
            using var mock = AutoMock.GetLoose(RegisterSmoothLevels);

            var models = Enumerable.Range(1, 5)
                .Select(x => new GraphInfoModel { Id = 1 })
                .ToArray();

            var viewModel = mock.Create<GraphTableViewModel>();

            await viewModel.SelectGraphsCommand.Execute(models);

            mock.Mock<IMessenger>()
                .Verify(x => x.Send(
                    It.IsAny<GraphSelectedMessage>(),
                    It.IsAny<IsAnyToken>()), Times.Once);
        }

        //[Test]
        //public async Task OnObstaclesChanged_ShouldChangeObstaclesCount()
        //{
        //    using var mock = AutoMock.GetLoose(RegisterSmoothLevels);
        //}

        private static void RegisterSmoothLevels(ContainerBuilder builder)
        {
            builder.RegisterType<SmoothLayer>().As<ILayer>()
                    .WithMetadata(MetadataKeys.SmoothKey, 0)
                    .WithMetadata(MetadataKeys.NameKey, SmoothLevelNames.No)
                    .SingleInstance();

            builder.RegisterType<SmoothLevelsViewModel>().AsSelf()
                .SingleInstance()
                .UsingConstructor(typeof(IEnumerable<Meta<ILayer>>))
                .WithAttributeFiltering();
        }
    }
}
