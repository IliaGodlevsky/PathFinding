using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.WPF._3D.Infrastructure.Commands
{
    internal sealed class InterruptAlgorithmCommand : BaseAlgorithmCommand
    {
        public InterruptAlgorithmCommand(PathfindingProcess algorithm) : base(algorithm)
        {

        }

        public override bool CanExecute(object parameter)
        {
            return algorithm.IsInProcess;
        }

        public override void Execute(object parameter)
        {
            algorithm.Interrupt();
        }
    }
}
