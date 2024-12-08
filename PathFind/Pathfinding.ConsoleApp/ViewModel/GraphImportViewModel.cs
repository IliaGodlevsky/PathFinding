using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Serialization;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphImportViewModel : BaseViewModel, IGraphImportViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog logger;
        private readonly ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer;

        public ReactiveCommand<Func<Stream>, Unit> ImportGraphCommand { get; }

        public GraphImportViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer,
            IRequestService<GraphVertexModel> service,
            ILog logger)
        {
            this.messenger = messenger;
            this.serializer = serializer;
            this.service = service;
            this.logger = logger;
            ImportGraphCommand = ReactiveCommand.CreateFromTask<Func<Stream>>(LoadGraph);
        }

        private async Task LoadGraph(Func<Stream> streamFactory)
        {
            await ExecuteSafe(async () =>
            {
                using var stream = streamFactory();
                if (stream != Stream.Null)
                {
                    var histories = await serializer.DeserializeFromAsync(stream).ConfigureAwait(false);
                    var result = await service.CreatePathfindingHistoriesAsync(histories).ConfigureAwait(false);
                    var graphs = result.Select(x => new GraphInfoModel()
                    {
                        Width = x.Graph.DimensionSizes.ElementAtOrDefault(0),
                        Length = x.Graph.DimensionSizes.ElementAtOrDefault(1),
                        Name = x.Graph.Name,
                        Neighborhood = x.Graph.Neighborhood,
                        Id = x.Graph.Id,
                        SmoothLevel = x.Graph.SmoothLevel,
                        ObstaclesCount = x.Graph.Vertices.GetObstaclesCount(),
                        Status = x.Graph.Status
                    }).ToArray();
                    messenger.Send(new GraphCreatedMessage(graphs));
                    logger.Info(graphs.Length > 0 ? "Graphs were loaded" : "Graph was loaded");
                }
            }, logger.Error).ConfigureAwait(false);
        }
    }
}
