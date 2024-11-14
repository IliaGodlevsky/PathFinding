using Autofac.Features.AttributeFilters;
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
        private readonly ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer;
        private readonly ILog logger;

        private int[] graphIds = Array.Empty<int>();
        private int[] GraphIds
        {
            get => graphIds;
            set => this.RaiseAndSetIfChanged(ref graphIds, value);
        }

        public ReactiveCommand<Stream, Unit> ExportGraphCommand { get; }

        public GraphExportViewModel(IRequestService<GraphVertexModel> service,
            ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger)
        {
            this.service = service;
            this.serializer = serializer;
            this.logger = logger;
            ExportGraphCommand = ReactiveCommand.CreateFromTask<Stream>(ExportGraph, CanExport());
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
        }

        private IObservable<bool> CanExport()
        {
            return this.WhenAnyValue(x => x.GraphIds, graphIds => graphIds.Length > 0);
        }

        private async Task ExportGraph(Stream stream)
        {
            await ExecuteSafe(async () =>
            {
                var graphs = await service.ReadSerializationHistoriesAsync(graphIds).ConfigureAwait(false);
                await serializer.SerializeToAsync(graphs, stream).ConfigureAwait(false);
                logger.Info(graphs.Count == 1 ? "Graph was saved" : "Graphs were saved");
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
