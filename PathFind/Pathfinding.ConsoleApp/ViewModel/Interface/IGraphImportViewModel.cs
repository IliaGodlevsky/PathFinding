using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using System;
using System.IO;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphImportViewModel
    {
        ReactiveCommand<Func<(Stream Stream, ExportFormat? Format)>, Unit> ImportGraphCommand { get; }
    }
}