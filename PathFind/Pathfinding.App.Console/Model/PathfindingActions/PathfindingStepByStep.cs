using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using System;

namespace Pathfinding.App.Console.Model.InProcessActions
{
    internal sealed class PathfindingStepByStep : IPathfindingAction
    {
        private readonly TimeSpan Wait = TimeSpan.FromMilliseconds(0.1);

        public void Do(PathfindingProcess algorithm)
        {
            if (algorithm.IsPaused)
            {
                algorithm.Resume();
            }
            Wait.Wait();
            algorithm.Pause();
        }
    }
}
