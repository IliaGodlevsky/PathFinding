using Pathfinding.App.Console.Model;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IRunsTableViewModel
    {
        ObservableCollection<RunInfoModel> Runs { get; }

        ReactiveCommand<int[], Unit> SelectRunsCommand { get; }
    }
}