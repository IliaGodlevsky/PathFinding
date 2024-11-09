using DynamicData;
using NStack;
using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class NeighborhoodFactoryUpdateView : FrameView
    {
        private readonly CompositeDisposable disposables = new();
        private readonly NeighborhoodFactoriesViewModel factoryViewModel;
        private readonly GraphUpdateViewModel viewModel;

        public NeighborhoodFactoryUpdateView(
            NeighborhoodFactoriesViewModel factoryViewModel,
            GraphUpdateViewModel viewModel)
        {
            this.factoryViewModel = factoryViewModel;
            this.viewModel = viewModel;
            Initialize();
            neighborhoods.RadioLabels = factoryViewModel.Factories
                .Keys.Select(ustring.Make).ToArray();
            neighborhoods.Events().SelectedItemChanged
                .Where(x => x.SelectedItem > -1 && !viewModel.IsReadOnly)
                .Select(x => factoryViewModel.Factories.Keys.ElementAt(x.SelectedItem))
                .BindTo(this.viewModel, x => x.Neighborhood)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Neighborhood)
                .Where(x => x != null)
                .Select(x => neighborhoods.RadioLabels.IndexOf(x))
                .BindTo(neighborhoods, x => x.SelectedItem)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.IsReadOnly)
                .Select(x => !x)
                .BindTo(this, x => x.Visible)
                .DisposeWith(disposables);
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            base.Dispose(disposing);
        }
    }
}
