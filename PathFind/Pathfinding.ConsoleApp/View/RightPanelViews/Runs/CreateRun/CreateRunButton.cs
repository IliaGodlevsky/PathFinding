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
using ReactiveUI;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal sealed partial class CreateRunButton : Button
    {
        private readonly IMessenger messenger;
        private readonly CompositeDisposable disposables = new();

        public CreateRunButton([KeyFilter(KeyFilters.Views)]IMessenger messenger)
        {
            X = Pos.Percent(25);
            Y = 0;
            Text = "Create";
            this.messenger = messenger;
            messenger.Register<RunViewModelChangedMessage>(this, OnRunViewModelChanged);
        }

        private void OnRunViewModelChanged(object recipient, RunViewModelChangedMessage msg)
        {
            SubscribeOnCreateCommand(msg.RunViewModel);
        }

        private void SubscribeOnCreateCommand(CreateRunViewModel viewModel)
        {
            disposables.Clear();
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .InvokeCommand(viewModel, x => x.CreateRunCommand)
                .DisposeWith(disposables);

        }
    }
}
