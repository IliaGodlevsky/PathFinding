using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.WPF._3D.Infrastructure.Commands
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
