using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class GraphExportView
    {
        private readonly CompositeDisposable disposables = [];

        public GraphExportView(IGraphExportViewModel viewModel)
        {
            Initialize();
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => new Func<StreamModel>(() =>
                {
                    var filePath = GetFilePath(viewModel);
                    return string.IsNullOrEmpty(filePath.Path)
                        ? new(Stream.Null, null)
                        : new(global::Pathfinding.App.Console.View.GraphExportView.OpenWrite(filePath.Path), filePath.Format);
                }))
                .InvokeCommand(viewModel, x => x.ExportGraphCommand)
                .DisposeWith(disposables);
        }

        private static FileStream OpenWrite(string path)
        {
            return new(path, FileMode.Create, FileAccess.Write, FileShare.None);
        }

        private static (string Path, StreamFormat? Format) GetFilePath(IGraphExportViewModel viewModel)
        {
            var formats = Enum.GetValues<StreamFormat>()
                .ToDictionary(x => x.ToExtensionRepresentation());
            var allowedTypes = formats.Keys.ToList();
            using var dialog = new SaveDialog("Export",
                "Enter file name", allowedTypes)
            {
                Width = Dim.Percent(45),
                Height = Dim.Percent(55)
            };
            var export = new ExportOptionsView(viewModel)
            {
                ColorScheme = dialog.ColorScheme,
                Width = Dim.Percent(50),
                Height = 2,
                X = Pos.Center(),
                Y = 5
            };
            dialog.Add(export);
            Application.Run(dialog);
            string filePath = dialog.FilePath.ToString();
            string extension = Path.GetExtension(filePath);
            return !dialog.Canceled && dialog.FilePath != null
                ? (filePath, formats[extension])
                : (string.Empty, null);
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            base.Dispose(disposing);
        }
    }
}
