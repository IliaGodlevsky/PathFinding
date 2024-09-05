using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels
{
    internal sealed class DeleteGraphButtonViewModel : BaseViewModel
    {
        private int graphId;
        private readonly IMessenger messenger;
        private readonly IRequestService<VertexModel> service;
        private readonly ILog logger;

        public int GraphId 
        {
            get => graphId;
            set => this.RaiseAndSetIfChanged(ref graphId, value);
        }

        public ReactiveCommand<MouseEventArgs, Unit> DeleteCommand { get; }

        public DeleteGraphButtonViewModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            IRequestService<VertexModel> service, 
            ILog logger)
        {
            DeleteCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(DeleteGraph, CanDelete());
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
        }

        private IObservable<bool> CanDelete()
        {
            return this.WhenAnyValue(x => x.GraphId,
                (graphId) => graphId > 0);
        }

        private async Task DeleteGraph(MouseEventArgs e)
        {
            await ExecuteSafe(async () =>
            {
                await service.DeleteGraphAsync(GraphId);
                messenger.Send(new GraphDeletedMessage(GraphId));
            }, logger.Error);
        }

        private void OnGraphSelected(object recipient, GraphSelectedMessage msg)
        {
            GraphId = msg.GraphId;
        }
    }
}
