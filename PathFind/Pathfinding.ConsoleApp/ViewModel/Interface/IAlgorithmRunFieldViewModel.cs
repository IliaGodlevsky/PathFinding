using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IAlgorithmRunFieldViewModel
    {
        AlgorithmRevisionModel SelectedRun { get; set; }

        IGraph<RunVertexModel> RunGraph { get; }
    }
}