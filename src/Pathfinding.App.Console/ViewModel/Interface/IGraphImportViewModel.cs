using Pathfinding.App.Console.Model;
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IGraphImportViewModel
    {
        ReactiveCommand<Func<StreamModel>, Unit> ImportGraphCommand { get; }
    }
}