using Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.View.ButtonsFrameViews
{
    internal sealed partial class LoadGraphButton : Button
    {
        private readonly LoadGraphButtonModel viewModel;

        public LoadGraphButton(LoadGraphButtonModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            this.Events()
                .MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .InvokeCommand(viewModel, x => x.LoadGraphCommand);
        }
    }
}
