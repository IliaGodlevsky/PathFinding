using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.WPF._2D.Infrastructure
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
