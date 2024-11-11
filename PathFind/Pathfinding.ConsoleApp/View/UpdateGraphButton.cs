using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using System;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class UpdateGraphButton : Button
    {
        private readonly IMessenger messenger;

        public UpdateGraphButton([KeyFilter(KeyFilters.Views)] IMessenger messenger,
            GraphUpdateViewModel viewModel)
        {
            this.messenger = messenger;
            X = Pos.Percent(16.67f);
            Y = 0;
            Width = Dim.Percent(16.67f);
            Text = "Update";
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Where(x => viewModel.SelectedGraphs.Length > 0)
                .Do(x => messenger.Send(new OpenGraphUpdateViewMessage()))
                .Subscribe();
        }
    }
}
