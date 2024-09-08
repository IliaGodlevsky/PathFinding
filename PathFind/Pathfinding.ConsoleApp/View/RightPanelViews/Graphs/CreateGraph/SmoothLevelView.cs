using NStack;
using System.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Pathfinding.ConsoleApp.ViewModel;
using System.Collections.Generic;
using Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class SmoothLevelView : FrameView
    {
        private readonly CreateGraphViewModel viewModel;
        private readonly SmoothLevelViewModel smoothLevelViewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public SmoothLevelView(CreateGraphViewModel viewModel,
            SmoothLevelViewModel smoothLevelViewModel)
        {
            this.viewModel = viewModel;
            this.smoothLevelViewModel = smoothLevelViewModel;
            Initialize();
            var smooths = smoothLevelViewModel.Levels
                .Select(x => x.Value)
                .ToArray();
            smoothLevels.RadioLabels = smoothLevelViewModel.Levels.Keys
                .Select(x => ustring.Make(x))
                .ToArray();
            smoothLevels.Events()
                .SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Select(x => smooths[x.SelectedItem])
                .BindTo(viewModel, x => x.SmoothLevel)
                .DisposeWith(disposables);
            smoothLevels.SelectedItem = 0;
            VisibleChanged += OnVisibilityChanged;
        }

        private void OnVisibilityChanged()
        {
            if (Visible)
            {
                smoothLevels.SelectedItem = 0;
            }
        }
    }
}
