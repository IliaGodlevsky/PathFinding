using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IAlgorithmRunFieldViewModel
    {
        float Fraction { get; set; }

        ReactiveCommand<float, Unit> ProcessToCommand { get; }

        ReactiveCommand<float, bool> ProcessNextCommand { get; }

        ReactiveCommand<float, bool> ReverseNextCommand { get; }

        IGraph<RunVertexModel> RunGraph { get; }
    }
}