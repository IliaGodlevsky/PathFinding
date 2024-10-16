using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Shared;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    [Order(1)]
    internal sealed partial class DijkstraAlgorithmListItem : Label
    {
        private readonly IMessenger messenger;
        private readonly CreateDijkstraRunViewModel viewModel;

        public DijkstraAlgorithmListItem(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            CreateDijkstraRunViewModel viewModel)
        {
            Text = viewModel.AlgorithmId;
            Initialize();
            this.messenger = messenger;
            this.viewModel = viewModel;
            MouseClick += OnViewClicked;
        }

        private void OnViewClicked(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked)
            {
                messenger.Send(new OpenStepRuleViewMessage());
                messenger.Send(new CloseHeuristicsViewMessage());
                messenger.Send(new CloseSpreadViewMessage());
                messenger.Send(new StepRuleViewModelChangedMessage(viewModel));
                messenger.Send(new RunViewModelChangedMessage(viewModel));
            }
        }
    }
}
