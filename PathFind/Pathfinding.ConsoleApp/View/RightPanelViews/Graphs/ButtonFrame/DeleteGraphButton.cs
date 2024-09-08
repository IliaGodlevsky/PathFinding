using Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Pathfinding.ConsoleApp.View.ButtonsFrameViews
{
    internal sealed partial class DeleteGraphButton : Button
    {
        private readonly DeleteGraphButtonViewModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public DeleteGraphButton(DeleteGraphButtonViewModel viewModel)
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
