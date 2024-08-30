using NStack;
using Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels;
using System.Linq;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using ReactiveUI;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class NeighborhoodFactoryView : FrameView
    {
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private readonly NeighborhoodFactoryViewModel viewModel;

        public NeighborhoodFactoryView(NeighborhoodFactoryViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            
            neighborhoods.RadioLabels = viewModel.Factories
                .Keys
                .Select(x => ustring.Make(x))
                .ToArray();
            neighborhoods.Events()
                .SelectedItemChanged
                .DistinctUntilChanged()
                .Where(x => x.SelectedItem > -1)
                .Select(x =>
                {
                    string label = neighborhoods.RadioLabels[x.SelectedItem].ToString();
                    return viewModel.Factories[label];
                })
                .BindTo(viewModel, x => x.NeighborhoodFactory)
                .DisposeWith(disposables);
        }
    }
}
