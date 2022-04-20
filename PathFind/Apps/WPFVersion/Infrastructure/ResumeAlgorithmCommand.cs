using Algorithm.Base;

namespace WPFVersion.Infrastructure
{
    internal sealed class ResumeAlgorithmCommand : BaseAlgorithmCommand
    {
        public ResumeAlgorithmCommand(PathfindingAlgorithm algorithm) : base(algorithm)
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
