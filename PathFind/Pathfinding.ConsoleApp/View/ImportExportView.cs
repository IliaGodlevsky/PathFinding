using Pathfinding.ConsoleApp.ViewModel;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class ImportExportView : FrameView
    {
        private readonly ImportExportViewModel viewModel;

        public ImportExportView(ImportExportViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
        }
    }
}
