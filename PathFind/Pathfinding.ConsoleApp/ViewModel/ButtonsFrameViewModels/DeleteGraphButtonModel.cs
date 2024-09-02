using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using Pathfinding.Service.Interface;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels
{
    internal sealed class DeleteGraphButtonModel : ReactiveObject
    {
        private int graphId;
        private readonly IMessenger messenger;
        private readonly IRequestService<VertexViewModel> service;

        public int GraphId 
        {
            get => graphId;
            set => this.RaiseAndSetIfChanged(ref graphId, value);
        }

        public ReactiveCommand<MouseEventArgs, Unit> DeleteCommand { get; }

        public DeleteGraphButtonModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            IRequestService<VertexViewModel> service)
        {
            DeleteCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(DeleteGraph, CanDelete());
            this.messenger = messenger;
            this.service = service;
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
        }

        private IObservable<bool> CanDelete()
        {
            return this.WhenAnyValue(x => x.GraphId,
                (graphId) => graphId > 0);
        }

        private async Task DeleteGraph(MouseEventArgs e)
        {
            await service.DeleteGraphAsync(GraphId);
            messenger.Send(new GraphDeletedMessage(GraphId));
        }

        private void OnGraphSelected(object recipient, GraphSelectedMessage msg)
        {
            GraphId = msg.GraphId;
        }
    }
}
