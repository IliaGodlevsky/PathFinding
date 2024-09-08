using Pathfinding.ConsoleApp.ViewModel;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class RunView : FrameView
    {
        private readonly RunViewModel viewModel;

        public RunView(RunViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}
