using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
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
    internal sealed class SaveGraphButtonModel : ReactiveObject
    {
        private readonly IRequestService<VertexViewModel> service;
        private readonly ISerializer<GraphSerializationModel> serializer;
        private readonly IMessenger messenger;

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

        public SaveGraphButtonModel(IRequestService<VertexViewModel> service,
            ISerializer<GraphSerializationModel> serializer,
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger)
        {
            this.service = service;
            this.serializer = serializer;
            this.messenger = messenger;
            SaveGraphCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(SaveGraph, CanSave());
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
            messenger.Register<GraphDeletedMessage>(this, OnGraphDeleted);
        }

        private IObservable<bool> CanSave()
        {
            return this.WhenAnyValue(
                x => x.GraphId,
                x => x.FilePath,
                (graphId, filePath) => graphId > 0 && !string.IsNullOrEmpty(filePath));
        }

        private async Task SaveGraph(MouseEventArgs e)
        {
            var graph = await service.ReadSerializationGraphAsync(GraphId);
            await serializer.SerializeToFileAsync(graph, FilePath);
            FilePath = string.Empty;
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
