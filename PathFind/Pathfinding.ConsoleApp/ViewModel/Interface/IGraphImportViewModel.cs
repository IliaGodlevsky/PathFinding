using ReactiveUI;
using System;
using System.IO;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphImportViewModel
    {
        ReactiveCommand<Func<Stream>, Unit> ImportGraphCommand { get; }
    }
}