using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class SaveGraphButton
    {
        private readonly SaveGraphButtonViewModel viewModel;
        private readonly CompositeDisposable disposables = new();

        public SaveGraphButton(SaveGraphButtonViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            this.Events().MouseClick
                .Where(x => viewModel.GraphIds.Length > 0)
                .Do(async x => await OnSaveButtonClicked())
                .InvokeCommand(viewModel, x => x.SaveGraphCommand)
                .DisposeWith(disposables);
        }

        private async Task OnSaveButtonClicked()
        {
            var allowedTypes = new List<string>() { ".json" };
            using var dialog = new SaveDialog("Export", "Enter file name", allowedTypes);
            dialog.Width = Dim.Percent(45);
            dialog.Height = Dim.Percent(55);
            Application.Run(dialog);
            if (!dialog.Canceled && dialog.FileName != null)
            {
                viewModel.FilePath = dialog.FilePath.ToString();
            }
            await Task.CompletedTask;
        }
    }
}
