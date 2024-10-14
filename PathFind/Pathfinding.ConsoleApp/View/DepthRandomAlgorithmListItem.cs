using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Shared;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    [Order(10)]
    internal sealed class DepthRandomAlgorithmListItem : Label
    {
        private readonly IMessenger messenger;
        private readonly CreateDepthRandomAlgorithmRunViewModel viewModel;

        public DepthRandomAlgorithmListItem(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            CreateDepthRandomAlgorithmRunViewModel viewModel)
        {
            Text = "DFS rand";
            Y = 1;
            X = 0;
            this.messenger = messenger;
            this.viewModel = viewModel;
            MouseClick += OnViewClicked;
        }

        private void OnViewClicked(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked)
            {
                messenger.Send(new CloseStepRulesViewMessage());
                messenger.Send(new CloseHeuristicsViewMessage());
                messenger.Send(new CloseSpreadViewMessage());
                messenger.Send(new RunViewModelChangedMessage(viewModel));
            }
        }
    }
}
