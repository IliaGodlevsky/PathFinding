using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Serialization;
using ReactiveUI;
using System;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels
{
    internal sealed class SaveGraphButtonModel
    {
        private readonly IRequestService<VertexViewModel> service;
        private readonly ISerializer<GraphSerializationModel> serializer;
        private readonly IMessenger messenger;

        public int GraphId { get; set; }

        public string FilePath { get; set; }

        public ReactiveCommand<Unit, Unit> SaveGraphCommand { get; }

        public SaveGraphButtonModel(IRequestService<VertexViewModel> service,
            ISerializer<GraphSerializationModel> serializer,
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger)
        {
            this.service = service;
            this.serializer = serializer;
            this.messenger = messenger;
            SaveGraphCommand = ReactiveCommand.Create(SaveGraph, CanSave());
        }

        private IObservable<bool> CanSave()
        {
            return this.WhenAnyValue(
                x => x.GraphId,
                x => x.FilePath,
                (graphId, filePath) => graphId > 0 && !string.IsNullOrEmpty(filePath));
        }

        private async void SaveGraph()
        {
            var graph = await service.ReadSerializationGraphAsync(GraphId);
            await serializer.SerializeToFileAsync(graph, FilePath);
        }
    }
}
