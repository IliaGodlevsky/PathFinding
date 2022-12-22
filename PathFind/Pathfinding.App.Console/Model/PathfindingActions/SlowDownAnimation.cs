﻿using Pathfinding.App.Console.Interface;
using Shared.Primitives.Extensions;
using System;

namespace Pathfinding.App.Console.Model.PathfindingActions
{
    internal sealed class SlowDownAnimation : IAnimationSpeedAction
    {
        private static readonly TimeSpan Step = TimeSpan.FromMilliseconds(1);

        public TimeSpan Do(TimeSpan current)
        {
            return Constants.AlgorithmDelayTimeValueRange.ReturnInRange(current + Step);
        }
    }
}