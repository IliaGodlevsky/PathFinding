using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.Console.Interface
{
    internal interface IPathfindingAction
    {
        void Do(PathfindingProcess algorithm);
    }
}
