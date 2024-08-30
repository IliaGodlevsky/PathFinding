using NStack;
using Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels;
using System.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class SmoothLevelView : FrameView
    {
        private readonly SmoothLevelsViewModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public SmoothLevelView(SmoothLevelsViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            var levels = viewModel.SmoothLevels.Keys.Select(x => ustring.Make(x)).ToArray();
            smoothLevels.RadioLabels = levels;
            smoothLevels.Events()
                .SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Select(x =>
                {
                    string key = smoothLevels.RadioLabels[x.SelectedItem].ToString();
                    return viewModel.SmoothLevels[key];
                })
                .BindTo(viewModel, x => x.SmoothLevel)
                .DisposeWith(disposables);
        }
    }
}
