using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [HighPriority]
    internal sealed class EnterObstaclePercentMenuItem : GraphMenuItem
    {
        public EnterObstaclePercentMenuItem(IMessenger messenger, IInput<int> input)
            : base(messenger, input)
        {
        }

        public override async Task ExecuteAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;
            using (Cursor.UseCurrentPositionWithClean())
            {
                int obstaclePercent = input.Input(Languages.ObstaclePercentInputMsg,
                    Constants.ObstaclesPercentValueRange);
                var msg = new ObstaclePercentMessage(obstaclePercent);
                messenger.Send(msg, Tokens.Graph);
                await Task.CompletedTask;
            }
        }

        public override string ToString()
        {
            return Languages.InputObstaclePercent;
        }
    }
}