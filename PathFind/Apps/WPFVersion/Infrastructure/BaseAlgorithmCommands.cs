using Algorithm.Base;

namespace WPFVersion.Infrastructure
{
    internal abstract class BaseAlgorithmCommand : BaseCommand
    {
        protected readonly PathfindingProcess algorithm;

        protected BaseAlgorithmCommand(PathfindingProcess algorithm)
        {
            this.algorithm = algorithm;
        }
    }
}
