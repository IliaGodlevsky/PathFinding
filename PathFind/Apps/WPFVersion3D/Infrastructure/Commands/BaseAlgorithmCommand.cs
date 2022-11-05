using Algorithm.Base;

namespace WPFVersion3D.Infrastructure.Commands
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
