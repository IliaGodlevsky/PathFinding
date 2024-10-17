using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class DeleteGraphButton : Button
    {
        private readonly GraphDeletionViewModel viewModel;
        private readonly CompositeDisposable disposables = new();

        public DeleteGraphButton(GraphDeletionViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .InvokeCommand(this.viewModel, x => x.DeleteCommand)
                .DisposeWith(disposables);
        }
    }
}
