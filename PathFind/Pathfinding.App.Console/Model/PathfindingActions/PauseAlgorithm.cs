using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console.Model.InProcessActions
{
    internal sealed class PauseAlgorithm : IPathfindingAction
    {
        public void Do(PathfindingProcess algorithm)
        {
            algorithm.Pause();
        }
    }
}
