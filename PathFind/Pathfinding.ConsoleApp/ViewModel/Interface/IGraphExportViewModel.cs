using ReactiveUI;
using System;
using System.IO;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphExportViewModel
    {
        ReactiveCommand<Func<Stream>, Unit> ExportGraphCommand { get; }
    }
}