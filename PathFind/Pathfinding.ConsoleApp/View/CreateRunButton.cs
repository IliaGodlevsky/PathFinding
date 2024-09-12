using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using System.Linq;
using System.Reactive.Disposables;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using System;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class CreateRunButton : Button
    {
        private readonly IMessenger messenger;
        private readonly CompositeDisposable disposables = new();

        public CreateRunButton([KeyFilter(KeyFilters.Views)]IMessenger messenger)
        {
            X = Pos.Percent(15);
            Y = 0;
            Text = "Create";
            this.messenger = messenger;
            messenger.Register<RunViewModelChangedMessage>(this, OnRunViewModelChanged);
            
        }

        private void OnRunViewModelChanged(object recipient, RunViewModelChangedMessage msg)
        {
            SubscribeOnCreateCommand(msg.RunViewModel);
        }

        private void SubscribeOnCreateCommand(CreateRunButtonViewModel viewModel)
        {
            disposables.Clear();
            var canExecute = viewModel.CreateRunCommand.CanExecute;
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Zip(viewModel.CreateRunCommand.CanExecute)
                .Where(x => x.Second)
                .Select(x => x.First)
                .Do(async x =>
                {
                    messenger.Send(new OpenRunViewMessage());
                    await viewModel.CreateRunCommand.Execute(x);
                    messenger.Send(new CloseRunCreationViewMessage());
                    disposables.Clear();
                })
                .Subscribe()
                .DisposeWith(disposables);
        }
    }
}
