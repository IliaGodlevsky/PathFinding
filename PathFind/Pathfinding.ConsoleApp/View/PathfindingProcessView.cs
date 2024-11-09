using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.ViewModel;
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
        private readonly IMessenger messenger;
        private readonly CompositeDisposable disposables = new();

        public PathfindingProcessView([KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            X = Pos.Percent(15);
            Y = 0;
            Text = "Create";
            this.messenger = messenger;
            messenger.Register<PathfindingViewModelChangedMessage>(this, OnPathfindingViewModelChanged);
        }

        private void OnPathfindingViewModelChanged(object recipient, PathfindingViewModelChangedMessage msg)
        {
            SubscribeOnCreateCommand(msg.ViewModel);
        }

        private void SubscribeOnCreateCommand(PathfindingProcessViewModel viewModel)
        {
            disposables.Clear();
            var canExecute = viewModel.StartAlgorithmCommand.CanExecute;
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(async x =>
                {
                    if (await canExecute.FirstOrDefaultAsync())
                    {
                        messenger.Send(new CloseAlgorithmCreationViewMessage());
                        await viewModel.StartAlgorithmCommand.Execute(x);
                    }
                })
                .Subscribe()
                .DisposeWith(disposables);
        }
    }
}
