using ReactiveUI;
using System.IO;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphExportViewModel
    {
        ReactiveCommand<Stream, Unit> ExportGraphCommand { get; }
    }
}