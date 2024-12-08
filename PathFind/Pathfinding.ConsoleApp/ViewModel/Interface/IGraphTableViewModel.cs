using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphTableViewModel
    {
        ObservableCollection<GraphInfoModel> Graphs { get; }

        ReactiveCommand<int, Unit> ActivateGraphCommand { get; }

        ReactiveCommand<int[], Unit> SelectGraphsCommand { get; }

        ReactiveCommand<Unit, Unit> LoadGraphsCommand { get; }
    }
}