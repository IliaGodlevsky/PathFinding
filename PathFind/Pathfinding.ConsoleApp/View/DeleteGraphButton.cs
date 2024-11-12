using Pathfinding.ConsoleApp.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class DeleteGraphButton : Button
    {
        private readonly IGraphDeleteViewModel viewModel;
        private readonly CompositeDisposable disposables = new();

        public DeleteGraphButton(IGraphDeleteViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => Unit.Default)
                .InvokeCommand(this.viewModel, x => x.DeleteCommand)
                .DisposeWith(disposables);
        }
    }
}
