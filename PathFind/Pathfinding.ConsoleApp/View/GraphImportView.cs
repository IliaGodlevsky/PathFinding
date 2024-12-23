using Pathfinding.ConsoleApp.Extensions;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.IO;
using System.Linq;
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
                .Select(x => new Func<(Stream Stream, ExportFormat? Format)>(() =>
                {
                    var fileName = GetFileName();
                    return string.IsNullOrEmpty(fileName.Path)
                        ? (Stream.Null, null)
                        : (File.OpenRead(fileName.Path), fileName.Format);
                }))
                .InvokeCommand(viewModel, x => x.ImportGraphCommand);
        }

        private static (string Path, ExportFormat? Format) GetFileName()
        {
            var formats = Enum
                .GetValues(typeof(ExportFormat))
                .Cast<ExportFormat>()
                .ToDictionary(x => x.ToExtensionRepresentation());
            var extensions = formats.Keys.ToList();
            using var dialog = new OpenDialog("Import",
                "Import graph", extensions)
            {
                Width = Dim.Percent(45),
                Height = Dim.Percent(55)
            };
            Application.Run(dialog);
            string filePath = dialog.FilePath.ToString();
            string extension = Path.GetExtension(filePath);
            return !dialog.Canceled && dialog.FilePath != null
                ? (filePath, formats[extension])
                : (string.Empty, null);
        }
    }
}
