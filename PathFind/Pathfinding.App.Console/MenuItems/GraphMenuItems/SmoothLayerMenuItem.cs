using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Smoothing;
using Pathfinding.GraphLib.Smoothing.Interface;
using Shared.Primitives.ValueRange;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class SmoothLayerMenuItem(IInput<int> input, 
        IMessenger messenger, IMeanCost meanCost) : IMenuItem
    {
        private static readonly InclusiveValueRange<int> SmoothLevelRange = new(1, 4);

        private readonly IInput<int> input = input;
        private readonly IMessenger messenger = messenger;
        private readonly IMeanCost meanCost = meanCost;

        public void Execute()
        {
            var menu = new[] { "No", "Low", "Medium", "High" }.CreateMenuList(1);
            string msg = string.Concat(menu, "\n", "Enter smooth level: ");
            using (Cursor.UseCurrentPositionWithClean())
            {
                int level = input.Input(msg, SmoothLevelRange);
                var smoothLayer = new SmoothLayer(meanCost);
                var repeat = Enumerable.Repeat(smoothLayer, level);
                var layers = new Layers(repeat);
                var message = new LayerMessage(layers);
                messenger.Send(message, Tokens.Graph);
            }
        }

        public override string ToString()
        {
            return Languages.SmoothGraphItem;
        }
    }
}
