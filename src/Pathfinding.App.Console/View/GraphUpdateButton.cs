﻿using Pathfinding.App.Console.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed class GraphUpdateButton : Button
    {
        public GraphUpdateButton(GraphUpdateView view, GraphUpdateViewModel viewModel)
        {
            X = Pos.Percent(16.67f);
            Y = 0;
            Width = Dim.Percent(16.67f);
            Text = "Update";
            viewModel.WhenAnyValue(x => x.SelectedGraphs)
                .Select(x => x.Length > 0)
                .BindTo(this, x => x.Enabled);
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(x => view.Visible = true)
                .Subscribe();
        }
    }
}
