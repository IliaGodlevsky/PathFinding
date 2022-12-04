using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.Interface;
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
