using Pathfinding.App.Console.Interface;
using Shared.Primitives.Extensions;
using System;

namespace Pathfinding.App.Console.Model.PathfindingActions
{
    internal sealed class SpeedUpAnimation : IAnimationSpeedAction
    {
        public TimeSpan Do(TimeSpan current)
        {
            return Constants.AlgorithmDelayTimeValueRange.ReturnInRange(current - TimeSpan.FromMilliseconds(1));
        }
    }
}
