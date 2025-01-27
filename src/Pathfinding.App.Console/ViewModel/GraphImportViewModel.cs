﻿using Autofac.Features.AttributeFilters;
using Autofac.Features.Metadata;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.ViewModel;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Resources;
using Pathfinding.App.Console.ViewModel.Interface;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Serialization;
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class GraphImportViewModel : BaseViewModel, IGraphImportViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog logger;
        private readonly IReadOnlyDictionary<StreamFormat,
            ISerializer<PathfindingHisotiriesSerializationModel>> serializers;

        public ReactiveCommand<Func<StreamModel>, Unit> ImportGraphCommand { get; }

        public GraphImportViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IEnumerable<Meta<ISerializer<PathfindingHisotiriesSerializationModel>>> serializers,
            IRequestService<GraphVertexModel> service,
            ILog logger)
        {
            this.messenger = messenger;
            this.serializers = serializers
                .ToDictionary(x => (StreamFormat)x.Metadata[MetadataKeys.ExportFormat], x => x.Value);
            this.service = service;
            this.logger = logger;
            ImportGraphCommand = ReactiveCommand.CreateFromTask<Func<StreamModel>>(LoadGraph);
        }

        private async Task LoadGraph(Func<StreamModel> streamFactory)
        {
            await ExecuteSafe(async () =>
            {
                var stream = streamFactory();
                var importFormat = stream.Format;
                var importStream = stream.Stream;
                using (importStream)
                {
                    if (importStream != Stream.Null && importFormat.HasValue)
                    {
                        var serializer = serializers[importFormat.Value];
                        var histories = await serializer.DeserializeFromAsync(importStream)
                            .ConfigureAwait(false);
                        var result = await service.CreatePathfindingHistoriesAsync(histories.Histories)
                            .ConfigureAwait(false);
                        var graphs = result.Select(x => x.Graph).ToGraphInfo();
                        messenger.Send(new GraphCreatedMessage(graphs));
                        logger.Info(graphs.Length > 0
                            ? Resource.WasLoadedMsg
                            : Resource.WereLoadedMsg);
                    }
                }
            }, logger.Error).ConfigureAwait(false);
        }
    }
}
