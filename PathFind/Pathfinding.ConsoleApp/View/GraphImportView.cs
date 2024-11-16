using Pathfinding.ConsoleApp.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using System;
using System.IO;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphImportView : Button
    {
        private readonly IGraphImportViewModel viewModel;

        public GraphImportView(IGraphImportViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(async x =>
                {
                    using var stream = GetStream();
                    if (stream != Stream.Null)
                    {
                        await viewModel.ImportGraphCommand.Execute(stream);
                    }
                })
                .Subscribe();
        }

        private static Stream GetStream()
        {
            using var dialog = new OpenDialog("Import",
                "Import graph", new() { ".dat" })
            {
                Width = Dim.Percent(45),
                Height = Dim.Percent(55)
            };
            Application.Run(dialog);
            return !dialog.Canceled && dialog.FilePath != null
                ? File.OpenRead(dialog.FilePath.ToString())
                : Stream.Null;
        }
    }
}
