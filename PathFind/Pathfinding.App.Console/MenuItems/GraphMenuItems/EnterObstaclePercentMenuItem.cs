using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class EnterObstaclePercentMenuItem : GraphMenuItem
    {
        public override int Order => 4;

        public EnterObstaclePercentMenuItem(IMessenger messenger, IInput<int> input) 
            : base(messenger, input)
        {
        }



        public override void Execute()
        {
            using (Cursor.CleanUpAfter())
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