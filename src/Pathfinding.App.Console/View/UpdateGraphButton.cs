using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using Pathfinding.App.Console.ViewModel;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed class UpdateGraphButton : Button
    {
        public UpdateGraphButton([KeyFilter(KeyFilters.Views)] IMessenger messenger,
            GraphUpdateViewModel viewModel)
        {
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
