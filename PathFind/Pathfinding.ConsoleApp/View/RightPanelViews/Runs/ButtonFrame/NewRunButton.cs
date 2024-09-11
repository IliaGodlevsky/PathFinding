using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using Pathfinding.ConsoleApp.ViewModel;
using System;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.ButtonFrame
{
    internal sealed partial class NewRunButton : Button
    {
        private readonly IMessenger messenger;
        private readonly NewRunButtonViewModel viewModel;

        public NewRunButton([KeyFilter(KeyFilters.Views)] IMessenger messenger,
            NewRunButtonViewModel viewModel)
        {
            Initialize();
            this.messenger = messenger;
            this.viewModel = viewModel;
            MouseClick += OnClick;
        }

        private void OnClick(MouseEventArgs e)
        {
            if(e.MouseEvent.Flags == MouseFlags.Button1Clicked && viewModel.CanCreate())
            {
                messenger.Send(new OpenRunCreationViewMessage());
            }
        }
    }
}
