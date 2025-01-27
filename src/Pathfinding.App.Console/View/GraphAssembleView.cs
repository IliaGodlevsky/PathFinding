using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using Pathfinding.App.Console.ViewModel.Interface;
using Pathfinding.Shared.Extensions;
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
        private readonly IGraphAssembleViewModel viewModel;
        private readonly CompositeDisposable disposables = new();
        private readonly Terminal.Gui.View[] children;

        public GraphAssembleView(
            [KeyFilter(KeyFilters.GraphAssembleView)] IEnumerable<Terminal.Gui.View> children,
            IGraphAssembleViewModel viewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            this.viewModel = viewModel;
            Initialize();
            this.children = children.ToArray();
            Add(this.children);
            var hideWindowCommand = ReactiveCommand.Create(Hide, this.viewModel.CreateCommand.CanExecute);
            var commands = new[] { hideWindowCommand, this.viewModel.CreateCommand };
            var combined = ReactiveCommand.CreateCombined(commands);
            createButton.Events()
                .MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => Unit.Default)
                .InvokeCommand(combined)
                .DisposeWith(disposables);

            cancelButton.MouseClick += OnCancelClicked;
            messenger.Register<OpenGraphAssembleViewMessage>(this, OnOpenCreateGraphViewRequestRecieved);
        }

        private void OnOpenCreateGraphViewRequestRecieved(object recipient,
            OpenGraphAssembleViewMessage request)
        {
            Visible = true;
            children.ForEach(x => x.Visible = true);
        }

        private void OnCancelClicked(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked)
            {
                Hide();
                Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
            }
        }

        private void Hide()
        {
            Visible = false;
            children.ForEach(x => x.Visible = false);
        }
    }
}
