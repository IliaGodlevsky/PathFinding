using DynamicData;
using NStack;
using Pathfinding.ConsoleApp.Extensions;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Core;
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

        public NeighborhoodFactoryUpdateView(GraphUpdateViewModel viewModel)
        {
            Initialize();
            var factories = Enum.GetValues(typeof(Neighborhoods))
                .Cast<Neighborhoods>()
                .ToDictionary(x => x.ToStringRepresentation());
            var radioLabels = factories.Keys.Select(ustring.Make).ToArray();
            var values = radioLabels.Select(x => factories[x.ToString()]).ToArray();
            neighborhoods.RadioLabels = radioLabels;
            neighborhoods.Events().SelectedItemChanged
                .Where(x => x.SelectedItem > -1 
                    && viewModel.Status == GraphStatuses.Editable)
                .Select(x => values[x.SelectedItem])
                .BindTo(viewModel, x => x.Neighborhood)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Neighborhood)
                .Select(x => factories.Values.IndexOf(x))
                .BindTo(neighborhoods, x => x.SelectedItem)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Status)
                .Select(x => x != GraphStatuses.Readonly)
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
