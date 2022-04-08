using Algorithm.Base;

namespace WPFVersion3D.Infrastructure.Commands
{
    internal sealed class InterruptAlgorithmCommand : BaseAlgorithmCommand
    {
        public InterruptAlgorithmCommand(PathfindingAlgorithm algorithm) : base(algorithm)
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
