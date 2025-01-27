using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using Pathfinding.App.Console.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class PathfindingProcessView : Button
    {
        private readonly CompositeDisposable disposables = new();

        public PathfindingProcessView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            IPathfindingProcessViewModel viewModel)
        {
            X = Pos.Percent(15);
            Y = 0;
            Text = "Create";
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(async x =>
                {
                    if (await viewModel.StartAlgorithmCommand.CanExecute.FirstOrDefaultAsync())
                    {
                        await viewModel.StartAlgorithmCommand.Execute();
                        messenger.Send(new CloseAlgorithmCreationViewMessage());
                    }
                })
                .Subscribe()
                .DisposeWith(disposables);
        }
    }
}
