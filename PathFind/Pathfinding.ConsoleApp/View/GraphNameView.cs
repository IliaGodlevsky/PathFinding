using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphNameView : FrameView
    {
        private readonly GraphAssembleViewModel viewModel;
        private readonly CompositeDisposable disposables = new();

        public GraphNameView(GraphAssembleViewModel viewModel)
        {
            Initialize();
            this.viewModel = viewModel;
            nameField.Events().TextChanged.Select(_ => nameField.Text)
                .BindTo(this.viewModel, x => x.Name)
                .DisposeWith(disposables);
            VisibleChanged += OnVisibilityChanged;
        }

        private void OnVisibilityChanged()
        {
            if (Visible)
            {
                nameField.Text = string.Empty;
            }
        }
    }
}
