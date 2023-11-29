﻿using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    [LowPriority]
    internal sealed class DepthFirstAlgorithmMenuItem : AlgorithmInputMenuItem
    {
        protected override string LanguageKey { get; } = nameof(Languages.DepthFirstAlgorithm);

        public DepthFirstAlgorithmMenuItem(IReadOnlyDictionary<string, IHeuristic> heuristics,
            IMessenger messenger, IInput<int> intInput) 
            : base(messenger, null, heuristics, intInput)
        {

        }

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new DepthFirstAlgorithmFactory(heuristics);
        }
    }
}