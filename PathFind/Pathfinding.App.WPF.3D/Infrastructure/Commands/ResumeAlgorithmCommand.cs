using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.WPF._3D.Infrastructure.Commands
{
    internal sealed class ResumeAlgorithmCommand : BaseAlgorithmCommand
    {
        public ResumeAlgorithmCommand(PathfindingProcess algorithm) : base(algorithm)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return algorithm.IsPaused;
        }

        public override void Execute(object parameter)
        {
            algorithm.Resume();
        }
    }
}
