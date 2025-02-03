﻿using Pathfinding.App.Console.Resources;
using Pathfinding.App.Console.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed class RunUpdateView : Button
    {
        private readonly CompositeDisposable disposables = [];

        public RunUpdateView(IRunUpdateViewModel viewModel)
        {
            X = Pos.Percent(33);
            Y = 0;
            Width = Dim.Percent(33);
            Text = Resource.UpdateAlgorithms;
            viewModel.UpdateRunsCommand.CanExecute
                .BindTo(this, x => x.Enabled)
                .DisposeWith(disposables);
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Throttle(TimeSpan.FromMilliseconds(30))
                .Select(x => Unit.Default)
                .InvokeCommand(viewModel, x => x.UpdateRunsCommand)
                .DisposeWith(disposables);
        }
    }
}
