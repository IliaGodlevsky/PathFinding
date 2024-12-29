using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using System;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphExportViewModel
    {
        ExportOptions Options { get; set; }

        ReactiveCommand<Func<StreamModel>, Unit> ExportGraphCommand { get; }
    }
}