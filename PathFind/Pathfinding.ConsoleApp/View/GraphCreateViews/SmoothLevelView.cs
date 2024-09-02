using NStack;
using System.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Pathfinding.ConsoleApp.ViewModel;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class SmoothLevelView : FrameView
    {
        private readonly CreateGraphViewModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public SmoothLevelView(CreateGraphViewModel viewModel,
            IEnumerable<(string Name, int Level)> levels)
        {
            this.viewModel = viewModel;
            Initialize();
            var smooths = levels
                .Select(x => x.Level)
                .ToArray();
            smoothLevels.RadioLabels = levels
                .Select(x => ustring.Make(x.Name))
                .ToArray();
            smoothLevels.Events()
                .SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Select(x => smooths[x.SelectedItem])
                .BindTo(viewModel, x => x.SmoothLevel)
                .DisposeWith(disposables);
            smoothLevels.SelectedItem = 0;
        }
    }
}
