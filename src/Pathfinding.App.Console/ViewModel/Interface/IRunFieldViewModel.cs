using Pathfinding.App.Console.Model;
using Pathfinding.Domain.Interface;
using System.Collections.ObjectModel;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IRunFieldViewModel
    {
        RunModel SelectedRun { get; set; }

        ObservableCollection<RunModel> Runs { get; }

        IGraph<RunVertexModel> RunGraph { get; }
    }
}