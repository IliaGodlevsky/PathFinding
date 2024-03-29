﻿using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Runtime.CompilerServices;

namespace Pathfinding.AlgorithmLib.Core.Realizations.StepRules
{
    public sealed class RatedStepRule : IStepRule
    {
        private readonly IStepRule stepRule;
        private readonly int rate;

        public RatedStepRule(IStepRule stepRule, int rate = 2)
        {
            this.stepRule = stepRule;
            this.rate = rate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return Math.Pow(stepRule.CalculateStepCost(neighbour, current), rate);
        }
    }
}