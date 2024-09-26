using CommunityToolkit.Mvvm.Messaging;
using Moq;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Logging.Loggers;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Shared.Extensions;
using Pathfinding.TestUtils.Attributes;
using ReactiveUI;

namespace Pathfinding.ConsoleApp.Test
{
    [TestFixture, UnitTest]
    public class GraphsTableViewModelTests
    {
        private IMessenger messenger;
        private Mock<IRequestService<VertexModel>> service;
        private GraphTableViewModel graphsTable;
        private readonly Dictionary<int, GraphModel<VertexModel>> graphs;

        public GraphsTableViewModelTests()
        {
            graphs = new Dictionary<int, GraphModel<VertexModel>>()
            {
                { 1, new(){ Id = 1, Name = "Test1",  Graph = Graph<VertexModel>.Empty } },
                { 2, new(){ Id = 2, Name = "Test2",  Graph = Graph<VertexModel>.Empty } },
                { 3, new(){ Id = 3, Name = "Test3",  Graph = Graph<VertexModel>.Empty } },
                { 4, new(){ Id = 4, Name = "Test4",  Graph = Graph<VertexModel>.Empty } },
                { 5, new(){ Id = 5, Name = "Test5",  Graph = Graph<VertexModel>.Empty } },
            };
        }

        [SetUp]
        public void SetUp()
        {
            service = new Mock<IRequestService<VertexModel>>();
            service.Setup(x => x.ReadGraphAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns<int, CancellationToken>((x, t) => Task.FromResult(graphs[x]));
            messenger = new WeakReferenceMessenger();
            graphsTable = new GraphTableViewModel(service.Object, messenger, new NullLog());
        }

        [Test]
        public void OnGraphCreated_SingleGraph_ShouldAppearInCollectionAndActivate()
        {
            var graphModel = graphs.First().Value;
            var models = graphModel.Enumerate().ToArray();
            bool isActivated = false;
            void OnActivated(object recipient, GraphActivatedMessage msg)
            {
                isActivated = msg.GraphId == 1;
            }

            messenger.Register<GraphActivatedMessage>(this, OnActivated);
            messenger.Send(new GraphCreatedMessage(models));

            Assert.Multiple(() =>
            {
                Assert.That(graphsTable.Graphs.Any(x => graphModel.Id == x.Id), Is.True);
                Assert.That(isActivated);
                service.Verify(service => service.ReadGraphAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()), Times.Once);
            });
        }

        [Test]
        public void OnGraphsCreated_ShouldAddAllInCollectionAndActivateFirst()
        {
            bool isActivated = false;
            void OnActivated(object recipient, GraphActivatedMessage msg)
            {
                isActivated = msg.GraphId == 1;
            }
            messenger.Register<GraphActivatedMessage>(this, OnActivated);

            messenger.Send(new GraphCreatedMessage(graphs.Values.ToArray()));

            Assert.Multiple(() =>
            {
                Assert.That(graphsTable.Graphs.Any(x => graphs.ContainsKey(x.Id)), Is.True);
                Assert.That(isActivated, Is.False);
                service.Verify(service => service.ReadGraphAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()), Times.Never);
            });
        }

        [Test]
        public void OnGraphDeleted_ExistingGraphs_ShouldRemoveFromList()
        {
            graphsTable.Graphs.AddRange(graphs.Values.Select(x => new GraphInfoModel()
            {
                Id = x.Id,
                Width = 0,
                Length = 0,
                Obstacles = 0,
                Name = x.Name
            }));
            var toDeleteIds = graphs.Keys.Skip(1).Take(2).ToArray();

            messenger.Send(new GraphsDeletedMessage(toDeleteIds));
            var difference = graphsTable.Graphs.ExceptBy(toDeleteIds, x => x.Id).ToArray();

            Assert.That(graphs.Count - toDeleteIds.Length, Is.EqualTo(difference.Length));
        }
    }
}
