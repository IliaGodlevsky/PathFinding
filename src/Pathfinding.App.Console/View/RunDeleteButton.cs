﻿using Pathfinding.App.Console.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class RunDeleteButton : Button
    {
        private readonly CompositeDisposable disposables = [];

        public RunDeleteButton(IRunDeleteViewModel viewModel)
        {
            Initialize();
            viewModel.WhenAnyValue(x => x.SelectedRunsIds)
                .Select(x => x.Length > 0)
                .BindTo(this, x => x.Enabled)
                .DisposeWith(disposables);
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => Unit.Default)
                .InvokeCommand(viewModel, x => x.DeleteRunsCommand)
                .DisposeWith(disposables);
        }
    }
}
