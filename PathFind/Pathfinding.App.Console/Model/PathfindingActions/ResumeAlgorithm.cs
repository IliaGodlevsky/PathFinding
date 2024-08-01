using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.Model.InProcessActions
{
    internal sealed class ResumeAlgorithm : IPathfindingAction
    {
        public void Do(PathfindingProcess algorithm)
        {
            algorithm.Resume();
        }
    }
}
