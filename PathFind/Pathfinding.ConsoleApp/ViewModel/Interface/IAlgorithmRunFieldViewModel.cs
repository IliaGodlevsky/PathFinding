using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;
using ReactiveUI;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IAlgorithmRunFieldViewModel
    {
        ReactiveCommand<int, bool> ProcessNextCommand { get; }

        ReactiveCommand<int, bool> ReverseNextCommand { get; }

        IGraph<RunVertexModel> RunGraph { get; }
    }
}