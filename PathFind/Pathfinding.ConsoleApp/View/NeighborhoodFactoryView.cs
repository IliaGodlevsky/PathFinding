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
    internal sealed partial class NeighborhoodFactoryView : FrameView
    {
        private readonly CompositeDisposable disposables = new();
        private readonly NeighborhoodFactoriesViewModel factoryViewModel;
        private readonly GraphAssembleViewModel viewModel;

        public NeighborhoodFactoryView(
            NeighborhoodFactoriesViewModel factoryViewModel,
            GraphAssembleViewModel viewModel)
        {
            this.factoryViewModel = factoryViewModel;
            this.viewModel = viewModel;
            Initialize();
            neighborhoods.RadioLabels = factoryViewModel.Factories.Keys
                .Select(ustring.Make).ToArray();
            neighborhoods.Events().SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Select(x =>
                {
                    var pair = factoryViewModel.Factories.ElementAt(x.SelectedItem);
                    return (Name: pair.Key, Factory: pair.Value);
                })
                .BindTo(this.viewModel, x => x.NeighborhoodFactory)
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
