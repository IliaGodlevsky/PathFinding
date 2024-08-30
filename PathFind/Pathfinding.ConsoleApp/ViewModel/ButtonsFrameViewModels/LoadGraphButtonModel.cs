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
    internal sealed class LoadGraphButtonModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<VertexViewModel> service;
        private readonly ISerializer<GraphSerializationModel> serializer;

        public string FileName { get; set; }

        public ReactiveCommand<Unit, Unit> LoadGraphCommand { get; }

        public LoadGraphButtonModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            ISerializer<GraphSerializationModel> serializer,
            IRequestService<VertexViewModel> service)
        {
            this.messenger = messenger;
            this.serializer = serializer;
            this.service = service;
            LoadGraphCommand = ReactiveCommand.Create(LoadGraph, CanLoad());
        }

        private IObservable<bool> CanLoad()
        {
            return this.WhenAnyValue(x => x.FileName,
                fileName => !string.IsNullOrEmpty(fileName));
        }

        private async void LoadGraph()
        {
            var graph = await serializer.DeserializeFromFileAsync(FileName);
            var result = await service.CreateGraphAsync(graph);
            //messenger.Send();
        }
    }
}
