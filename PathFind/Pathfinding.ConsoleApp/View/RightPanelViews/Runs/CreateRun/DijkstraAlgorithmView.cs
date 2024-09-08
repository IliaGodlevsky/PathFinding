using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal sealed partial class DijkstraAlgorithmView : Label
    {
        private readonly IMessenger messenger;
        private readonly CreateDijkstraRunViewModel viewModel;

        public DijkstraAlgorithmView([KeyFilter(KeyFilters.Views)]IMessenger messenger,
            CreateDijkstraRunViewModel viewModel)
        {
            Initialize();
            this.messenger = messenger;
            this.viewModel = viewModel;
            MouseClick += OnViewClicked;
        }

        private void OnViewClicked(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked)
            {
                IOpenViewMessage msg = new OpenDijkstraAlgorithmCreateViewRequest();
                var runViewModelChanged = new RunViewModelChangedMessage(viewModel);
                messenger.Send(msg);
                messenger.Send(runViewModelChanged);
            }
        }
    }
}
