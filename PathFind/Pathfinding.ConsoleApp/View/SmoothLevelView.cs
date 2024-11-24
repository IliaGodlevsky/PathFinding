using NStack;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.ConsoleApp.ViewModel.Interface;
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
        private readonly IRequireSmoothLevelViewModel viewModel;
        private readonly SmoothLevelsViewModel smoothLevelViewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public SmoothLevelView(IRequireSmoothLevelViewModel viewModel,
            SmoothLevelsViewModel smoothLevelViewModel)
        {
            this.viewModel = viewModel;
            this.smoothLevelViewModel = smoothLevelViewModel;
            Initialize();
            smoothLevels.RadioLabels = smoothLevelViewModel.Levels.Keys
                .Select(ustring.Make)
                .ToArray();
            smoothLevels.Events()
                .SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Select(x =>smoothLevelViewModel.Levels.Keys.ElementAt(x.SelectedItem))
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
