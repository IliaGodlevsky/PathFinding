using Pathfinding.App.Console.Interface;
using Pathfinding.Infrastructure.Business.Algorithms;
using Shared.Primitives.Single;

namespace Pathfinding.App.Console.Model.PathfindingActions
{
    internal sealed class NullPathfindingAction : Singleton<NullPathfindingAction, IPathfindingAction>, IPathfindingAction
    {
        private NullPathfindingAction()
        {

        }

        public void Do(PathfindingProcess algorithm)
        {

        }
    }
}
