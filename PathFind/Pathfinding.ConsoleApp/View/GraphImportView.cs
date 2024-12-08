using Pathfinding.ConsoleApp.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.IO;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphImportView : Button
    {
        public GraphImportView(IGraphImportViewModel viewModel)
        {
            Initialize();
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => new Func<Stream>(() =>
                {
                    var fileName = GetFileName();
                    return string.IsNullOrEmpty(fileName)
                        ? Stream.Null
                        : File.OpenRead(fileName);
                }))
                .InvokeCommand(viewModel, x => x.ImportGraphCommand);
        }

        private static string GetFileName()
        {
            using var dialog = new OpenDialog("Import",
                "Import graph", new() { ".dat" })
            {
                Width = Dim.Percent(45),
                Height = Dim.Percent(55)
            };
            Application.Run(dialog);
            return !dialog.Canceled && dialog.FilePath != null
                ? dialog.FilePath.ToString()
                : string.Empty;
        }
    }
}
