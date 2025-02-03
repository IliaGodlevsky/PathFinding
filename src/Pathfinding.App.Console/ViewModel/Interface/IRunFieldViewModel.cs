using Pathfinding.App.Console.Model;
using Pathfinding.Domain.Interface;
using System.Collections.ObjectModel;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IRunFieldViewModel
    {
        AlgorithmRevisionModel SelectedRun { get; set; }

        ObservableCollection<AlgorithmRevisionModel> Runs { get; }

        IGraph<RunVertexModel> RunGraph { get; }
    }
}