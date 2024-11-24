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
    internal sealed partial class NeighborhoodFactoryView : FrameView
    {
        private readonly CompositeDisposable disposables = new();
        private readonly NeighborhoodFactoriesViewModel factoryViewModel;
        private readonly IRequireNeighborhoodNameViewModel viewModel;

        public NeighborhoodFactoryView(
            NeighborhoodFactoriesViewModel factoryViewModel,
            IRequireNeighborhoodNameViewModel viewModel)
        {
            this.factoryViewModel = factoryViewModel;
            this.viewModel = viewModel;
            Initialize();
            neighborhoods.RadioLabels = factoryViewModel.Factories.Keys
                .Select(ustring.Make).ToArray();
            neighborhoods.Events().SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Select(x => factoryViewModel.Factories.Keys.ElementAt(x.SelectedItem))
                .BindTo(this.viewModel, x => x.Neighborhood)
                .DisposeWith(disposables);
            neighborhoods.SelectedItem = 0;
            VisibleChanged += OnVisibilityChanged;
        }

        private void OnVisibilityChanged()
        {
            if (Visible)
            {
                neighborhoods.SelectedItem = 0;
            }
        }
    }
}
