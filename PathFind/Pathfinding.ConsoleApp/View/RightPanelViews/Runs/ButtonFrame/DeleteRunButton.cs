using Pathfinding.ConsoleApp.ViewModel.RightPanelViewModels.RunViewModels;
using System.Reactive.Disposables;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.ButtonFrame
{
    internal sealed partial class DeleteRunButton : Button
    {
        private readonly CompositeDisposable disposables = new();

        public DeleteRunButton(DeleteRunButtonViewModel viewModel)
        {
            Initialize();
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .InvokeCommand(viewModel, x => x.DeleteRunCommand)
                .DisposeWith(disposables);
        }
    }
}
