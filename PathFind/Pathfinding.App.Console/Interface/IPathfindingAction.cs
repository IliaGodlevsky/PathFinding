using Pathfinding.Infrastructure.Business.Algorithms;

namespace Pathfinding.App.Console.Interface
{
    internal interface IPathfindingAction
    {
        void Do(PathfindingProcess algorithm);
    }
}
