using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphImportView : Button
    {
        private readonly GraphImportViewModel viewModel;

        public GraphImportView(GraphImportViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            this.Events()
                .MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(async x => await OnLoadButtonClicked())
                .InvokeCommand(viewModel, x => x.LoadGraphCommand);
        }

        private async Task OnLoadButtonClicked()
        {
            var allowedTypes = new List<string>() { ".json", ".dat" };
            using var dialog = new OpenDialog("Import", string.Empty, allowedTypes);
            dialog.Width = Dim.Percent(45);
            dialog.Height = Dim.Percent(55);
            Application.Run(dialog);
            if (!dialog.Canceled && dialog.FilePath != null)
            {
                viewModel.FilePath = dialog.FilePath.ToString();
            }
            await Task.CompletedTask;
        }
    }
}
