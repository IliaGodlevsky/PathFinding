using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphUpdateView : FrameView
    {
        private readonly GraphUpdateViewModel viewModel;
        private readonly IMessenger messenger;
        private readonly CompositeDisposable disposables = new();
        private readonly Terminal.Gui.View[] children;

        public GraphUpdateView(
            [KeyFilter(KeyFilters.GraphUpdateView)] IEnumerable<Terminal.Gui.View> children,
            GraphUpdateViewModel viewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            this.viewModel = viewModel;
            this.messenger = messenger;
            Initialize();
            this.children = children.ToArray();
            Add(this.children);
            updateButton.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => Unit.Default)
                .Do(async x =>
                {
                    if (await viewModel.UpdateGraphCommand.CanExecute.FirstOrDefaultAsync())
                    {
                        Hide();
                        await viewModel.UpdateGraphCommand.Execute();
                    }
                })
                .Subscribe()
                .DisposeWith(disposables);

            cancelButton.MouseClick += OnCancelClicked;
            messenger.Register<OpenGraphUpdateViewMessage>(this, OnOpenCreateGraphViewRequestRecieved);
        }

        private void OnOpenCreateGraphViewRequestRecieved(object recipient,
            OpenGraphUpdateViewMessage request)
        {
            Visible = true;
        }

        private void OnCancelClicked(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked)
            {
                Hide();
            }
        }

        private Unit Hide()
        {
            Visible = false;
            Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
            return Unit.Default;
        }
    }
}
