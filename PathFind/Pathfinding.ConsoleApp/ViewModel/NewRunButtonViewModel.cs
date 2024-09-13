using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using ReactiveUI;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class NewRunButtonViewModel : ReactiveObject
    {
        private readonly IMessenger messenger;

        public bool CanCreate() => graphId > 0;

        private int graphId;
        private int GraphId
        {
            get => graphId;
            set => this.RaiseAndSetIfChanged(ref graphId, value);
        }

        public NewRunButtonViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger)
        {
            this.messenger = messenger;
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            GraphId = msg.GraphId;
        }
    }
}
