using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class LeeAlgorithmListItem : Label
    {
        private readonly IMessenger messenger;
        private readonly CreateLeeAlgorithmRunViewModel viewModel;

        public LeeAlgorithmListItem([KeyFilter(KeyFilters.Views)] IMessenger messenger,
            CreateLeeAlgorithmRunViewModel viewModel)
        {
            Text = "Lee";
            this.messenger = messenger;
            this.viewModel = viewModel;
            MouseClick += OnViewClicked;
        }

        private void OnViewClicked(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked)
            {
                messenger.Send(new OpenRunCreationViewMessage());
                messenger.Send(new CloseStepRulesViewMessage());
                messenger.Send(new CloseHeuristicsViewMessage());
                messenger.Send(new CloseSpreadViewMessage());
                messenger.Send(new RunViewModelChangedMessage(viewModel));
            }
        }
    }
}
