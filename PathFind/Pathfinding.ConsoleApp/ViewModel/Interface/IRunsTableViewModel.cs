using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IRunsTableViewModel
    {
        ObservableCollection<RunInfoModel> Runs { get; }

        ReactiveCommand<RunInfoModel[], Unit> SelectRunsCommand { get; }
    }
}