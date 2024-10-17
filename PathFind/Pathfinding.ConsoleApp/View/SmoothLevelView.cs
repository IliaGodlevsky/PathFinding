using NStack;
using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class SmoothLevelView : FrameView
    {
        private readonly GraphAssembleViewModel viewModel;
        private readonly SmoothLevelViewModel smoothLevelViewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public SmoothLevelView(GraphAssembleViewModel viewModel,
            SmoothLevelViewModel smoothLevelViewModel)
        {
            this.viewModel = viewModel;
            this.smoothLevelViewModel = smoothLevelViewModel;
            Initialize();
            smoothLevels.RadioLabels = smoothLevelViewModel.Levels.Keys
                .Select(x => ustring.Make(x))
                .ToArray();
            smoothLevels.Events()
                .SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Select(x =>
                {
                    var pair = smoothLevelViewModel.Levels.ElementAt(x.SelectedItem);
                    return (Name: pair.Key, SmoothLevel: pair.Value);
                })
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
