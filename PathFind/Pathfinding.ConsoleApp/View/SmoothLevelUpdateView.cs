using NStack;
using Pathfinding.ConsoleApp.ViewModel;
using System.Linq;
using System.Reactive.Disposables;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using DynamicData;
using System;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class SmoothLevelUpdateView : FrameView
    {
        private readonly GraphUpdateViewModel viewModel;
        private readonly SmoothLevelViewModel smoothLevelViewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public SmoothLevelUpdateView(
            GraphUpdateViewModel viewModel,
            SmoothLevelViewModel smoothLevelViewModel)
        {
            this.viewModel = viewModel;
            this.smoothLevelViewModel = smoothLevelViewModel;
            Initialize();
            smoothLevels.RadioLabels = smoothLevelViewModel.Levels
                .Keys.Select(x => ustring.Make(x)).ToArray();
            smoothLevels.Events()
                .SelectedItemChanged
                .Where(x => x.SelectedItem > -1 && !viewModel.IsReadOnly)
                .Select(x => smoothLevelViewModel.Levels.Keys.ElementAt(x.SelectedItem))
                .BindTo(viewModel, x => x.SmoothLevel)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.SmoothLevel)
                .Where(x => x != null)
                .Select(x => smoothLevels.RadioLabels.IndexOf(x))
                .BindTo(smoothLevels, x => x.SelectedItem)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.IsReadOnly)
                .Select(x => !x)
                .BindTo(this, x => x.Visible);
        }
    }
}
