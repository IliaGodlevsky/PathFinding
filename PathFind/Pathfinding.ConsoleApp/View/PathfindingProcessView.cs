using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
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
