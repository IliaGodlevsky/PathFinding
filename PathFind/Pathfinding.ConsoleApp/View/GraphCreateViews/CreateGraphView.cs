using Pathfinding.ConsoleApp.ViewModel;
using System.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Messages;
using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class CreateGraphView : FrameView
    {
        private readonly CreateGraphViewModel viewModel;
        private readonly IMessenger messenger;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public CreateGraphView([KeyFilter(KeyFilters.CreateGraphView)]IEnumerable<Terminal.Gui.View> children,
            CreateGraphViewModel viewModel,
            [KeyFilter(KeyFilters.Views)]IMessenger messenger)
        {
            this.viewModel = viewModel;
            this.messenger = messenger;
            Initialize();
            Add(children.ToArray());
            this.createButton.Events()
                .MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .InvokeCommand(this.viewModel, x => x.CreateCommand)
                .DisposeWith(disposables);
            //createButton.MouseClick += e =>
            //{
            //    viewModel.CreateGraph(e);
            //};

            void OnCancelClicked(MouseEventArgs e)
            {
                if (e.MouseEvent.Flags == MouseFlags.Button1Clicked)
                {
                    Visible = false;
                }
            }

            cancelButton.MouseClick += OnCancelClicked;
            messenger.Register<OpenGraphCreateViewRequest>(this, OnOpenCreateGraphViewRequestRecieved);
        }

        private void OnOpenCreateGraphViewRequestRecieved(object recipient,
            OpenGraphCreateViewRequest request)
        {
            Visible = true;
        }
    }
}
