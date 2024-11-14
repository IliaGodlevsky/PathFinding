using ReactiveUI;
using System.IO;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphImportViewModel
    {
        ReactiveCommand<Stream, Unit> ImportGraphCommand { get; }
    }
}