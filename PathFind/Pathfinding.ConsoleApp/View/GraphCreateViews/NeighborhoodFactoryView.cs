using NStack;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using ReactiveUI;
using Terminal.Gui;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Interface.Factories;
using System.Collections.Generic;
using ReactiveMarbles.ObservableEvents;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class NeighborhoodFactoryView : FrameView
    {
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private readonly CreateGraphViewModel viewModel;

        public NeighborhoodFactoryView(
            IEnumerable<(string Name, INeighborhoodFactory Factory)> factories,
            CreateGraphViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            var neighbors = factories.Select(x => x.Factory).ToList();
            neighborhoods.RadioLabels = factories
                .Select(x => ustring.Make(x.Name))
                .ToArray();
            neighborhoods.Events().SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Select(x => neighbors[x.SelectedItem])
                .BindTo(this.viewModel, x => x.NeighborhoodFactory)
                .DisposeWith(disposables);
            neighborhoods.SelectedItem = 0;
        }
    }
}
