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
    internal sealed partial class SmoothLevelUpdateView : FrameView
    {
        private readonly CompositeDisposable disposables = new();

        public SmoothLevelUpdateView(GraphUpdateViewModel viewModel)
        {
            var lvls = Enum.GetValues(typeof(SmoothLevels))
                .Cast<SmoothLevels>()
                .ToDictionary(x => x.ToStringRepresentation());
            Initialize();
            var labels = lvls.Keys.Select(ustring.Make).ToArray();
            var values = labels.Select(x => lvls[x.ToString()]).ToList();
            smoothLevels.RadioLabels = labels;
            smoothLevels.Events()
                .SelectedItemChanged
                .Where(x => x.SelectedItem > -1 && viewModel.Status == GraphStatuses.Editable)
                .Select(x => values[x.SelectedItem])
                .BindTo(viewModel, x => x.SmoothLevel)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.SmoothLevel)
                .Select(x => values.IndexOf(x))
                .BindTo(smoothLevels, x => x.SelectedItem)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Status)
                .Select(x => x == GraphStatuses.Readonly ? false : true)
                .BindTo(this, x => x.Visible);
        }
    }
}
