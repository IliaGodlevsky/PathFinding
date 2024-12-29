using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using System;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphImportViewModel
    {
        ReactiveCommand<Func<StreamModel>, Unit> ImportGraphCommand { get; }
    }
}