using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.WPF._2D.Infrastructure
{
    internal sealed class PauseAlgorithmCommand : BaseAlgorithmCommand
    {
        public PauseAlgorithmCommand(PathfindingProcess algorithm) : base(algorithm)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return algorithm.IsInProcess && !algorithm.IsPaused;
        }

        public override void Execute(object parameter)
        {
            algorithm.Pause();
        }
    }
}
