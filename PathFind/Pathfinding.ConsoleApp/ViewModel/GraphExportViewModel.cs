using Autofac.Features.AttributeFilters;
using Autofac.Features.Metadata;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
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
    internal sealed class GraphExportViewModel : BaseViewModel, IGraphExportViewModel
    {
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog logger;
        private readonly IReadOnlyDictionary<ExportFormat, 
            ISerializer<PathfindingHisotiriesSerializationModel>> serializers;

        private int[] graphIds = Array.Empty<int>();
        private int[] GraphIds
        {
            get => graphIds;
            set => this.RaiseAndSetIfChanged(ref graphIds, value);
        }

        public ExportOptions Options { get; set; }

        public ReactiveCommand<Func<(Stream Stream, ExportFormat? Format)>, Unit> ExportGraphCommand { get; }

        public GraphExportViewModel(IRequestService<GraphVertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IEnumerable<Meta<ISerializer<PathfindingHisotiriesSerializationModel>>> serializers,
            ILog logger)
        {
            this.service = service;
            this.logger = logger;
            this.serializers =  serializers
                .ToDictionary(x => (ExportFormat)x.Metadata[MetadataKeys.ExportFormat], x => x.Value);
            ExportGraphCommand = ReactiveCommand.CreateFromTask<Func<(Stream, ExportFormat?)>>(ExportGraph, CanExport());
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
        }

        private IObservable<bool> CanExport()
        {
            return this.WhenAnyValue(x => x.GraphIds, graphIds => graphIds.Length > 0);
        }

        private async Task ExportGraph(Func<(Stream Stream, ExportFormat? Format)> streamFactory)
        {
            await ExecuteSafe(async () =>
            {
                var stream = streamFactory();
                var format = stream.Format;
                var serializationStream = stream.Stream;
                using (serializationStream)
                {
                    if (serializationStream != Stream.Null && format != null)
                    {
                        var serializer = serializers[format.Value];
                        var historiesTask = Options switch
                        {
                            ExportOptions.GraphOnly => service.ReadSerializationGraphsAsync(GraphIds),
                            ExportOptions.WithRange => service.ReadSerializationGraphsWithRangeAsync(GraphIds),
                            ExportOptions.WithRuns => service.ReadSerializationHistoriesAsync(GraphIds),
                            _ => throw new InvalidOperationException("Invalid export option"),
                        };
                        var histories = await historiesTask.ConfigureAwait(false);
                        await serializer.SerializeToAsync(histories, serializationStream)
                            .ConfigureAwait(false);
                        logger.Info(histories.Histories.Count == 1
                            ? "Graph was saved" 
                            : "Graphs were saved");
                    }
                }
            }, logger.Error).ConfigureAwait(false);
        }

        private void OnGraphSelected(object recipient, GraphSelectedMessage msg)
        {
            GraphIds = msg.Graphs.Select(x => x.Id).ToArray();
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            GraphIds = GraphIds.Except(msg.GraphIds).ToArray();
        }
    }
}
