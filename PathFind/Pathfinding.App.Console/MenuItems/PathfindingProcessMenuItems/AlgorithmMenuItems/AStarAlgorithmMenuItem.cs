﻿using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.DAL;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    [HighPriority]
    internal sealed class AStarAlgorithmMenuItem(
        IReadOnlyDictionary<string, IStepRule> stepRules,
        IReadOnlyDictionary<string, IHeuristic> heuristics,
        IInput<int> input,
        IMessenger messenger)
        : AlgorithmInputMenuItem(messenger, stepRules, heuristics, input)
    {
        protected override string LanguageKey { get; } = AlgorithmNames.AStar;

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new AStarAlgorithmFactory(stepRule, heuristics);
        }
    }
}
