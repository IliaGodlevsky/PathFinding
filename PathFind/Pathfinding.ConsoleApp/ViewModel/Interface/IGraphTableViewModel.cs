using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphTableViewModel
    {
        ObservableCollection<GraphInfoModel> Graphs { get; }

        ReactiveCommand<GraphInfoModel, Unit> ActivateGraphCommand { get; }

        ReactiveCommand<GraphInfoModel[], Unit> SelectGraphsCommand { get; }

        ReactiveCommand<Unit, Unit> LoadGraphsCommand { get; }
    }
}