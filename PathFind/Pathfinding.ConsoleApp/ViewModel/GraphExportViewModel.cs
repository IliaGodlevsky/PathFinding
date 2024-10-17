using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Shared.Extensions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphExportViewModel : BaseViewModel
    {
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer;
        private readonly ILog logger;

        private int[] graphIds = Array.Empty<int>();
        public int[] GraphIds
        {
            get => graphIds;
            set => this.RaiseAndSetIfChanged(ref graphIds, value);
        }

        private string filePath;
        public string FilePath
        {
            get => filePath;
            set => this.RaiseAndSetIfChanged(ref filePath, value);
        }

        public ReactiveCommand<MouseEventArgs, Unit> ExportGraphCommand { get; }

        public GraphExportViewModel(IRequestService<GraphVertexModel> service,
            ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger)
        {
            this.service = service;
            this.serializer = serializer;
            this.logger = logger;
            ExportGraphCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(ExportGraph, CanExport());
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
        }

        private IObservable<bool> CanExport()
        {
            return this.WhenAnyValue(
                x => x.GraphIds,
                x => x.FilePath,
                (graphIds, path) => graphIds.Length > 0 && !string.IsNullOrEmpty(path));
        }

        private async Task ExportGraph(MouseEventArgs e)
        {
            await ExecuteSafe(async () =>
            {
                var graphs = await service.ReadSerializationHistoriesAsync(graphIds)
                    .ConfigureAwait(false);
                await serializer.SerializeToFileAsync(graphs.ToList(), FilePath)
                    .ConfigureAwait(false);
                FilePath = string.Empty;
                logger.Info(graphs.Count == 1 ? "Graph was saved" : "Graphs were saved");
            }, logger.Error).ConfigureAwait(false);

        }

        private void OnGraphSelected(object recipient, GraphSelectedMessage msg)
        {
            GraphIds = msg.GraphIds;
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            GraphIds = GraphIds.Except(msg.GraphIds).ToArray();
        }
    }
}
