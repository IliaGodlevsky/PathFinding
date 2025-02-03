﻿using Autofac.Features.AttributeFilters;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class GraphAssembleView : FrameView
    {
        private readonly CompositeDisposable disposables = [];
        private readonly Terminal.Gui.View[] children;

        public GraphAssembleView(
            [KeyFilter(KeyFilters.GraphAssembleView)] IEnumerable<Terminal.Gui.View> children,
            IGraphAssembleViewModel viewModel)
        {
            Initialize();
            this.children = children.ToArray();
            Add(this.children);
            viewModel.CreateCommand.CanExecute
                .BindTo(createButton, x => x.Enabled)
                .DisposeWith(disposables);
            createButton.Events()
                .MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => Unit.Default)
                .Do(x => Visible = false)
                .InvokeCommand(viewModel, x => x.CreateCommand)
                .DisposeWith(disposables);
            foreach (var child in children)
            {
                this.Events().VisibleChanged
                    .Select(x => Visible)
                    .BindTo(child, x => x.Visible)
                    .DisposeWith(disposables);
            }
            cancelButton.Events().MouseClick
                .Where(e => e.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(x =>
                {
                    Visible = false;
                    Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
                })
                .Subscribe()
                .DisposeWith(disposables);
        }
    }
}
