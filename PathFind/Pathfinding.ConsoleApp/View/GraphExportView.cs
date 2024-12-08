using Pathfinding.ConsoleApp.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphExportView
    {
        private readonly CompositeDisposable disposables = new();

        public GraphExportView(IGraphExportViewModel viewModel)
        {
            Initialize();
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => new Func<Stream>(()=> 
                {
                    var filePath = GetFilePath();
                    return string.IsNullOrEmpty(filePath) 
                        ? Stream.Null 
                        : File.OpenWrite(filePath);
                }))
                .InvokeCommand(viewModel, x => x.ExportGraphCommand)
                .DisposeWith(disposables);
        }

        private static string GetFilePath()
        {
            using var dialog = new SaveDialog("Export",
                "Enter file name", new() { ".dat" })
            {
                Width = Dim.Percent(45),
                Height = Dim.Percent(55)
            };
            Application.Run(dialog);
            return !dialog.Canceled && dialog.FilePath != null
                ? dialog.FilePath.ToString()
                : string.Empty;
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            base.Dispose(disposing);
        }
    }
}
