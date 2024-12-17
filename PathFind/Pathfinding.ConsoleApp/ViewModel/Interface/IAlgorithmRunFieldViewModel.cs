using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;
using System.Collections.ObjectModel;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IAlgorithmRunFieldViewModel
    {
        AlgorithmRevisionModel SelectedRun { get; set; }

        ObservableCollection<AlgorithmRevisionModel> Runs { get; }

        IGraph<RunVertexModel> RunGraph { get; }
    }
}