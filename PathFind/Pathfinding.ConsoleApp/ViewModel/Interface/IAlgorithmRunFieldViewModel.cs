using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IAlgorithmRunFieldViewModel
    {
        float Fraction { get; set; }

        RunInfoModel SelectedRun { get; set; }

        ReactiveCommand<float, Unit> ProcessToCommand { get; }

        ReactiveCommand<float, Unit> ProcessNextCommand { get; }

        ReactiveCommand<float, Unit> ReverseNextCommand { get; }

        IGraph<RunVertexModel> RunGraph { get; }
    }
}