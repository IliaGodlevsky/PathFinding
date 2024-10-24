using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Shared;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    [Order(9)]
    internal sealed class AStarGreedyAlgorithmView : Label
    {
        private readonly IMessenger messenger;
        private readonly AStarGreedyAlgorithmViewModel viewModel;

        public AStarGreedyAlgorithmView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            AStarGreedyAlgorithmViewModel viewModel)
        {
            Text = viewModel.AlgorithmId;
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
                messenger.Send(new OpenStepRuleViewMessage());
                messenger.Send(new OpenHeuristicsViewMessage());
                messenger.Send(new CloseSpreadViewMessage());
                messenger.Send(new StepRuleViewModelChangedMessage(viewModel));
                messenger.Send(new HeuristicsViewModelChangedMessage(viewModel));
                messenger.Send(new PathfindingViewModelChangedMessage(viewModel));
            }
        }
    }
}
