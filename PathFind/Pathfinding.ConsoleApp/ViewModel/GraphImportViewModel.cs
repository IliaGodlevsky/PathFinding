using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
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
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphImportViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly Func<string, Stream> streamProvider;
        private readonly ILog logger;
        private readonly ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer;

        private string filePath;
        public string FilePath
        {
            get => filePath;
            set => this.RaiseAndSetIfChanged(ref filePath, value);
        }

        public ReactiveCommand<MouseEventArgs, Unit> LoadGraphCommand { get; }

        public GraphImportViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer,
            IRequestService<GraphVertexModel> service,
            ILog logger) : this(messenger, serializer,
                service, File.OpenRead, logger)
        {
        }

        public GraphImportViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer,
            IRequestService<GraphVertexModel> service,
            Func<string, Stream> streamProvider,
            ILog logger)
        {
            this.messenger = messenger;
            this.serializer = serializer;
            this.service = service;
            this.logger = logger;
            this.streamProvider = streamProvider;
            LoadGraphCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(LoadGraph, CanLoad());
        }

        private IObservable<bool> CanLoad()
        {
            return this.WhenAnyValue(x => x.FilePath,
                path => !string.IsNullOrEmpty(path));
        }

        private async Task LoadGraph(MouseEventArgs e)
        {
            await ExecuteSafe(async () =>
            {
                using var fileStream = streamProvider(FilePath);
                var histories = await serializer.DeserializeFromAsync(fileStream)
                    .ConfigureAwait(false);
                var result = await Task.Run(() => service.CreatePathfindingHistoriesAsync(histories))
                    .ConfigureAwait(false);
                var graphs = result.Select(x => x.Graph).ToArray();
                messenger.Send(new GraphCreatedMessage(graphs));
                logger.Info(graphs.Length > 0 ? "Graphs were loaded" : "Graph was loaded");
            }, logger.Error).ConfigureAwait(false);
        }
    }
}
