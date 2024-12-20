﻿using NStack;
using Pathfinding.ConsoleApp.Extensions;
using Pathfinding.ConsoleApp.ViewModel.Interface;
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
    internal sealed partial class SmoothLevelView : FrameView
    {
        private readonly CompositeDisposable disposables = new();

        public SmoothLevelView(IRequireSmoothLevelViewModel viewModel)
        {
            var smoothLevels = Enum.GetValues(typeof(SmoothLevels))
                .Cast<SmoothLevels>()
                .ToDictionary(x => x.ToStringRepresentation());
            Initialize();
            var labels = smoothLevels.Keys.Select(ustring.Make).ToArray();
            var values = labels.Select(x => smoothLevels[x.ToString()]).ToList();
            this.smoothLevels.RadioLabels = labels;
            this.smoothLevels.Events()
                .SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Select(x => values[x.SelectedItem])
                .BindTo(viewModel, x => x.SmoothLevel)
                .DisposeWith(disposables);
            this.smoothLevels.SelectedItem = 0;
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
