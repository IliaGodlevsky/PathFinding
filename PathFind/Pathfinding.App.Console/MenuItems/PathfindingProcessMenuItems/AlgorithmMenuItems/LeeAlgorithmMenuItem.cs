﻿using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms.Factories;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [MediumPriority]
    internal sealed class LeeAlgorithmMenuItem(IMessenger messenger)
        : AlgorithmMenuItem(messenger)
    {
        protected override string LanguageKey => AlgorithmNames.Lee;

        protected override AlgorithmInfo GetAlgorithm()
        {
            return new(new LeeAlgorithmFactory(), LanguageKey);
        }
    }
}
