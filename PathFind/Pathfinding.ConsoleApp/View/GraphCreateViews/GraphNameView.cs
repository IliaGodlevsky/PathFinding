using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using System.Reactive.Disposables;
using Pathfinding.ConsoleApp.ViewModel;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class GraphNameView : FrameView
    {
        private readonly CreateGraphViewModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public GraphNameView(CreateGraphViewModel viewModel)
        {
            Initialize();
            this.viewModel = viewModel;
            nameField.Events().TextChanged.Select(_ => nameField.Text)
                .BindTo(this.viewModel, x => x.Name)
                .DisposeWith(disposables);
        }
    }
}
