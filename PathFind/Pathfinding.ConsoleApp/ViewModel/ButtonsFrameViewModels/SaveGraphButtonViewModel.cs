using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Serialization;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels
{
    internal sealed class SaveGraphButtonViewModel : ImportExportViewModel
    {
        private readonly IRequestService<VertexModel> service;
        private readonly ISerializer<PathfindingHistorySerializationModel> serializer;
        private readonly IMessenger messenger;
        private readonly ILog logger;

        private int graphId;
        public int GraphId
        {
            get => graphId;
            set => this.RaiseAndSetIfChanged(ref graphId, value);
        }

        private string filePath;
        public string FilePath 
        {
            get => filePath;
            set => this.RaiseAndSetIfChanged(ref filePath, value); 
        }

        public ReactiveCommand<MouseEventArgs, Unit> SaveGraphCommand { get; }

        public SaveGraphButtonViewModel(IRequestService<VertexModel> service,
            ISerializer<PathfindingHistorySerializationModel> serializer,
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            ILog logger)
        {
            this.service = service;
            this.serializer = serializer;
            this.logger = logger;
            SaveGraphCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(SaveGraph, CanSave());
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
            messenger.Register<GraphDeletedMessage>(this, OnGraphDeleted);
        }

        private IObservable<bool> CanSave()
        {
            return this.WhenAnyValue(
                x => x.GraphId,
                graphId => graphId > 0);
        }

        private async Task SaveGraph(MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                await ExecuteSafe(async () =>
                {
                    var graph = await service.ReadSerializationHistoryAsync(GraphId);
                    await serializer.SerializeToFileAsync(graph, FilePath);
                    FilePath = string.Empty;
                }, logger.Error);
            }
        }

        private void OnGraphSelected(object recipient, GraphSelectedMessage msg)
        {
            GraphId = msg.GraphId;
        }

        private void OnGraphDeleted(object recipient, GraphDeletedMessage msg)
        {
            GraphId = 0;
        }
    }
}
