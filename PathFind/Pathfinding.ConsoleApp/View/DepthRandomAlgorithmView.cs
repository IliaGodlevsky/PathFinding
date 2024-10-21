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
    internal sealed class DepthRandomAlgorithmView : Label
    {
        private readonly IMessenger messenger;
        private readonly DepthRandomAlgorithmViewModel viewModel;

        public DepthRandomAlgorithmView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            DepthRandomAlgorithmViewModel viewModel)
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
                messenger.Send(new CloseStepRulesViewMessage());
                messenger.Send(new CloseHeuristicsViewMessage());
                messenger.Send(new CloseSpreadViewMessage());
                messenger.Send(new PathfindingViewModelChangedMessage(viewModel));
            }
        }
    }
}
