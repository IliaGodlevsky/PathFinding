using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.Model.InProcessActions
{
    internal sealed class InterruptAlgorithm : IPathfindingAction
    {
        public void Do(PathfindingProcess algorithm)
        {
            algorithm.Interrupt();
        }
    }
}
