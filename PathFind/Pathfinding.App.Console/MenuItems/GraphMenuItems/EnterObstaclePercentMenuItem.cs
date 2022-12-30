﻿using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [Order(4)]
    internal sealed class EnterObstaclePercentMenuItem : GraphMenuItem
    {
        public EnterObstaclePercentMenuItem(IMessenger messenger, IInput<int> input)
            : base(messenger, input)
        {
        }



        public override void Execute()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                int obstaclePercent = input.Input(Languages.ObstaclePercentInputMsg, Constants.ObstaclesPercentValueRange);
                messenger.Send(new ObstaclePercentMessage(obstaclePercent));
            }
        }

        public override string ToString()
        {
            return Languages.InputObstaclePercent;
        }
    }
}