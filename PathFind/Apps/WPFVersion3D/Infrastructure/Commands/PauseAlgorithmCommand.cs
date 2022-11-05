using Algorithm.Base;

namespace WPFVersion3D.Infrastructure.Commands
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
