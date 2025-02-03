﻿using Pathfinding.App.Console.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class GraphDeleteButton : Button
    {
        private readonly CompositeDisposable disposables = [];

        public GraphDeleteButton(IGraphDeleteViewModel viewModel)
        {
            Initialize();
            viewModel.DeleteCommand.CanExecute
                .BindTo(this, x => x.Enabled)
                .DisposeWith(disposables);
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => Unit.Default)
                .InvokeCommand(viewModel, x => x.DeleteCommand)
                .DisposeWith(disposables);
        }
    }
}
