using Pathfinding.ConsoleApp.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using System;
using System.IO;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphExportView
    {
        private readonly IGraphExportViewModel viewModel;

        public GraphExportView(IGraphExportViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(async x =>
                {
                    if (await viewModel.ExportGraphCommand.CanExecute.FirstOrDefaultAsync())
                    {
                        using var stream = GetStream();
                        if (stream != Stream.Null)
                        {
                            await viewModel.ExportGraphCommand.Execute(stream);
                        }
                    }
                })
                .Subscribe();
        }

        private static Stream GetStream()
        {
            using var dialog = new SaveDialog("Export",
                "Enter file name", new() { ".dat" })
            {
                Width = Dim.Percent(45),
                Height = Dim.Percent(55)
            };
            Application.Run(dialog);
            return !dialog.Canceled && dialog.FilePath != null
                ? File.OpenWrite(dialog.FilePath.ToString())
                : Stream.Null;
        }
    }
}
