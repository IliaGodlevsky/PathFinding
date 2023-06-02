using Pathfinding.App.Console.Interface;
using Shared.Primitives.Extensions;
using System;

namespace Pathfinding.App.Console.Model.PathfindingActions
{
    internal sealed class SpeedUpAnimation : IAnimationSpeedAction
    {
        private static readonly TimeSpan Millisecond = TimeSpan.FromMilliseconds(1);

        public TimeSpan Do(TimeSpan current)
        {
            var range = Constants.AlgorithmDelayTimeValueRange;
            var time = current - Millisecond;
            return range.ReturnInRange(time);
        }
    }
}
