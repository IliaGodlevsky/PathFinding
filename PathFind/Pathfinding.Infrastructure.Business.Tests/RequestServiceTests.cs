﻿using Autofac;
using Autofac.Extras.Moq;
using Bogus;
using Moq;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Domain.Interface.Repositories;

namespace Pathfinding.Infrastructure.Business.Tests
{
    [Category("Unit")]
    internal class RequestServiceTests
    {
        [Test]
        public async Task ReadAllGraphInfoAsync_ShouldReturnValidInfo()
        {
            var faker = new Faker<Graph>()
                .UseSeed(Environment.TickCount)
                .RuleFor(x => x.Name, x => x.Person.UserName)
                .RuleFor(x => x.Id, x => x.IndexFaker)
                .RuleFor(x => x.SmoothLevel, x => x.Random.Enum<SmoothLevels>())
                .RuleFor(x => x.Status, x => x.Random.Enum<GraphStatuses>())
                .RuleFor(x => x.Dimensions, x => $"[{x.Random.Int(20, 100)},{x.Random.Int(20, 100)}]")
                .RuleFor(x => x.Neighborhood, x => x.Random.Enum<Neighborhoods>());
            var graphs = faker.Generate(10);
            var obstaclesCount = (IReadOnlyDictionary<int, int>)graphs.ToDictionary(x => x.Id, x => 25);
            var mock = AutoMock.GetLoose();
            mock.Mock<IGraphParametresRepository>()
                .Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(graphs.AsEnumerable()));
            mock.Mock<IGraphParametresRepository>()
                .Setup(x => x.ReadObstaclesCountAsync(
                    It.Is<IEnumerable<int>>(x => x.SequenceEqual(graphs.Select(x => x.Id))),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(obstaclesCount));
            mock.Mock<IUnitOfWork>()
                .Setup(x => x.GraphRepository)
                .Returns(mock.Container.Resolve<IGraphParametresRepository>());
            mock.Mock<IUnitOfWorkFactory>()
                .Setup(x => x.Create())
                .Returns(mock.Container.Resolve<IUnitOfWork>());

            var requestService = mock.Create<RequestService<FakeVertex>>();

            var result = await requestService.ReadAllGraphInfoAsync();

            Assert.Multiple(() =>
            {
                mock.Mock<IUnitOfWorkFactory>().Verify(x => x.Create(), Times.Once());
                mock.Mock<IUnitOfWork>().Verify(x => x.GraphRepository, Times.Exactly(2));
                mock.Mock<IGraphParametresRepository>()
                    .Verify(x => x.GetAll(It.IsAny<CancellationToken>()), Times.Once());
                mock.Mock<IGraphParametresRepository>()
                    .Verify(x => x.ReadObstaclesCountAsync(
                        It.Is<IEnumerable<int>>(x => x.SequenceEqual(graphs.Select(x => x.Id))),
                        It.IsAny<CancellationToken>()), Times.Once());
                Assert.That(result.All(x => graphs.Any(y => y.Id == x.Id)
                    && result.First(y => y.Id == x.Id).ObstaclesCount == obstaclesCount[x.Id]));
                Assert.That(result.Count == graphs.Count);
            });
        }
    }
}
