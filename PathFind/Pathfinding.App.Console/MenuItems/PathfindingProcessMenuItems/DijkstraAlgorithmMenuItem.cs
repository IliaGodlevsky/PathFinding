using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [HighestPriority]
    internal sealed class DijkstraAlgorithmMenuItem : IMenuItem
    {
        private readonly IReadOnlyList<IStepRule> stepRules;
        private readonly IInput<int> input;
        private readonly IMessenger messenger;

        public DijkstraAlgorithmMenuItem(IReadOnlyList<IStepRule> stepRules,
            IInput<int> input, IMessenger messenger)
        {
            this.stepRules = stepRules;
            this.input = input;
            this.messenger = messenger;
        }

        public void Execute()
        {
            string menu = stepRules.CreateMenuList(1).ToString();
            int index = ChooseStepRule(menu, stepRules.Count);
            var stepRule = stepRules[index];
            var factory = new DijkstraAlgorithmFactory(stepRule);
            messenger.SendData(factory, Tokens.Pathfinding);
        }

        private int ChooseStepRule(string menu, int limit)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = input.Input(menu, limit, 1) - 1;
                return index;
            }
        }

        public override string ToString()
        {
            return "Dijkstra's algorithm";
        }
    }
}
