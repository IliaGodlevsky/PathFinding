﻿using Autofac;
using Autofac.Extras.Moq;
using Autofac.Features.Metadata;
using CommunityToolkit.Mvvm.Messaging;
using Moq;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.ViewModel;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.ConsoleApp.Tests;
using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Shared.Extensions;
using System.Linq.Expressions;
using System.Reactive.Linq;

namespace Pathfinding.App.Console.Tests.ViewModelTests
{
    [Category("Unit")]
    internal class GraphExportViewModelTests
    {
        private static async Task ExportGraphCommand_HasGraphs_ShouldExport(Expression<Func<IRequestService<GraphVertexModel>, Task<PathfindingHisotiriesSerializationModel>>> expression,
            ExportOptions options)
        {
            using var mock = AutoMock.GetLoose();

            GraphInfoModel[] models =
            [
                new GraphInfoModel() { Id = 1 },
                new GraphInfoModel() { Id = 2 },
                new GraphInfoModel() { Id = 3 }
            ];

            PathfindingHisotiriesSerializationModel histories
               = Enumerable.Range(1, 5)
               .Select(x => new PathfindingHistorySerializationModel())
               .ToArray()
               .To(x => new PathfindingHisotiriesSerializationModel() { Histories = x.ToList() });

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(expression)
                .Returns(Task.FromResult(histories));

            mock.Mock<IMessenger>().Setup(x => x.Register(
                    It.IsAny<object>(),
                    It.IsAny<IsAnyToken>(),
                    It.IsAny<MessageHandler<object, GraphSelectedMessage>>()))
               .Callback<object, object, MessageHandler<object, GraphSelectedMessage>>((r, t, handler)
                    => handler(r, new GraphSelectedMessage(models)));

            var serializer = mock.Mock<ISerializer<PathfindingHisotiriesSerializationModel>>();
            var meta = new Meta<ISerializer<PathfindingHisotiriesSerializationModel>>(serializer.Object, new Dictionary<string, object>()
            {
                { MetadataKeys.ExportFormat, StreamFormat.Binary }
            });
            var typedParam = new TypedParameter(typeof(IEnumerable<Meta<ISerializer<PathfindingHisotiriesSerializationModel>>>), new[] { meta });

            var viewModel = mock.Create<GraphExportViewModel>(typedParam);
            viewModel.Options = options;

            var command = viewModel.ExportGraphCommand;
            if (await command.CanExecute.FirstOrDefaultAsync())
            {
                await command.Execute(() => new(new MemoryStream(), StreamFormat.Binary));
            }

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(expression, Times.Once);

                mock.Mock<IMessenger>()
                    .Verify(x => x.Register(
                        It.IsAny<object>(),
                        It.IsAny<IsAnyToken>(),
                        It.IsAny<MessageHandler<object, GraphSelectedMessage>>()), Times.Once);

                serializer.Verify(x => x.SerializeToAsync(
                    It.IsAny<PathfindingHisotiriesSerializationModel>(),
                    It.IsAny<Stream>(),
                    It.IsAny<CancellationToken>()), Times.Once);
            });
        }

        [Test]
        public async Task ExportGraphCommand_WithRuns_ShouldExport()
        {
            await ExportGraphCommand_HasGraphs_ShouldExport(x => x.ReadSerializationHistoriesAsync(
                It.IsAny<IEnumerable<int>>(),
                It.IsAny<CancellationToken>()),
                ExportOptions.WithRuns);
        }

        [Test]
        public async Task ExportGraphCommand_WithRange_ShouldExport()
        {
            await ExportGraphCommand_HasGraphs_ShouldExport(x => x.ReadSerializationGraphsWithRangeAsync(
                It.IsAny<IEnumerable<int>>(),
                It.IsAny<CancellationToken>()),
                ExportOptions.WithRange);
        }

        [Test]
        public async Task ExportGraphCommand_GraphOnly_ShouldExport()
        {
            await ExportGraphCommand_HasGraphs_ShouldExport(x => x.ReadSerializationGraphsAsync(
                It.IsAny<IEnumerable<int>>(),
                It.IsAny<CancellationToken>()),
                ExportOptions.GraphOnly);
        }

        [Test]
        public async Task ExportGraphCommand_NullStream_ShouldNotExport()
        {
            using var mock = AutoMock.GetLoose();

            var serializer = mock.Mock<ISerializer<PathfindingHisotiriesSerializationModel>>();
            var meta = new Meta<ISerializer<PathfindingHisotiriesSerializationModel>>(serializer.Object, new Dictionary<string, object>()
            {
                { MetadataKeys.ExportFormat, StreamFormat.Binary }
            });
            var typedParam = new TypedParameter(typeof(IEnumerable<Meta<ISerializer<PathfindingHisotiriesSerializationModel>>>), new[] { meta });

            var viewModel = mock.Create<GraphExportViewModel>(typedParam);

            var command = viewModel.ExportGraphCommand;
            if (await command.CanExecute.FirstOrDefaultAsync())
            {
                await command.Execute(() => new(Stream.Null, null));
            }

            Assert.Multiple(() =>
            {
                mock.Mock<IRequestService<GraphVertexModel>>()
                    .Verify(x => x.ReadSerializationHistoriesAsync(
                        It.IsAny<IEnumerable<int>>(),
                        It.IsAny<CancellationToken>()), Times.Never);

                serializer.Verify(x => x.SerializeToAsync(
                    It.IsAny<PathfindingHisotiriesSerializationModel>(),
                    It.IsAny<Stream>(),
                    It.IsAny<CancellationToken>()), Times.Never);
            });
        }

        [Test]
        public async Task ExportGraphCommand_ThrowsException_ShouldLogError()
        {
            using var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterType<BinarySerializer<PathfindingHisotiriesSerializationModel>>()
                    .As<ISerializer<PathfindingHisotiriesSerializationModel>>()
                    .SingleInstance().WithMetadata(MetadataKeys.ExportFormat, StreamFormat.Binary);
            });

            mock.Mock<IRequestService<GraphVertexModel>>()
                .Setup(x => x.ReadSerializationHistoriesAsync(
                    It.IsAny<IEnumerable<int>>(),
                    It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var viewModel = mock.Create<GraphExportViewModel>();

            var command = viewModel.ExportGraphCommand;
            await command.Execute(() => new(new MemoryStream(), StreamFormat.Binary));

            mock.Mock<ILog>()
                .Verify(x => x.Error(
                    It.IsAny<Exception>(),
                    It.IsAny<string>()), Times.Once);
        }
    }
}
