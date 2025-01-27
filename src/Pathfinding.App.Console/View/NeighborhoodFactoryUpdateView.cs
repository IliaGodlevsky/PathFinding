﻿using DynamicData;
using NStack;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.Domain.Core;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
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
                .Where(x => x.SelectedItem > -1)
                .Select(x => values[x.SelectedItem])
                .BindTo(viewModel, x => x.Neighborhood)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Neighborhood)
                .Select(x => factories.Values.IndexOf(x))
                .BindTo(neighborhoods, x => x.SelectedItem)
                .DisposeWith(disposables);
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            base.Dispose(disposing);
        }
    }
}
