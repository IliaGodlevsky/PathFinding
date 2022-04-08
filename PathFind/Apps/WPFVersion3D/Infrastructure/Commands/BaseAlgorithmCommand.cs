using Algorithm.Base;

namespace WPFVersion3D.Infrastructure.Commands
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
