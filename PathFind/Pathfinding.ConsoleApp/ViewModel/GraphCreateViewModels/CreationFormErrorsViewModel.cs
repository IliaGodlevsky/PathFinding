using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using ReactiveUI;

namespace Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels
{
    internal sealed class CreationFormErrorsViewModel : ReactiveObject
    {
        private string message;

        public string Message 
        {
            get => message;
            set => this.RaiseAndSetIfChanged(ref message, value);
        }

        public CreationFormErrorsViewModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger)
        {
            messenger.Register<GraphFormErrorMessage>(this, RecieveErrorMessage);
        }

        private void RecieveErrorMessage(object recipient, GraphFormErrorMessage msg)
        {
            Message = msg.ErrorMessage;
        }
    }
}
