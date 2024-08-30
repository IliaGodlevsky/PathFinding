using Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class GraphNameView : FrameView
    {
        private readonly GraphNameViewModel viewModel;

        public GraphNameView(GraphNameViewModel viewModel)
        {
            Initialize();
            this.viewModel = viewModel;
        }
    }
}
