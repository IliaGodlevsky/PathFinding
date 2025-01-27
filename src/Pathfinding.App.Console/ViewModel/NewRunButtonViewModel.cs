using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.ViewModel;
using ReactiveUI;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class NewRunButtonViewModel : ReactiveObject
    {
        public bool CanCreate() => graphId > 0;

        private int graphId;
        public int GraphId
        {
            get => graphId;
            set => this.RaiseAndSetIfChanged(ref graphId, value);
        }

        public NewRunButtonViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger)
        {
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            GraphId = msg.Graph.Id;
        }
    }
}
