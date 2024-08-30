using Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Pathfinding.ConsoleApp.View.ButtonsFrameViews
{
    internal sealed partial class SaveGraphButton : Button
    {
        private readonly SaveGraphButtonModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public SaveGraphButton(SaveGraphButtonModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            this.Events().MouseClick
                .Where(e => e.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(OnSaveButtonClicked)
                .InvokeCommand(viewModel, x => x.SaveGraphCommand)
                .DisposeWith(disposables);
        }

        private void OnSaveButtonClicked(MouseEventArgs e)
        {
            using (var dialog = new SaveDialog())
            {
                Application.Run(dialog);
                viewModel.FilePath = dialog.FilePath.ToString();
            }
        }
    }
}
