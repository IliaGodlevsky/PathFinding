using Algorithm.Base;

namespace WPFVersion.Infrastructure
{
    internal abstract class BaseAlgorithmCommand : BaseCommand
    {
        protected readonly PathfindingAlgorithm algorithm;

        protected BaseAlgorithmCommand(PathfindingAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }
    }
}
