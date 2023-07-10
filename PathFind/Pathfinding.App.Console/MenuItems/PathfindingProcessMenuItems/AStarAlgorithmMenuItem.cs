using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [HighPriority]
    internal sealed class AStarAlgorithmMenuItem : IMenuItem
    {
        private readonly IReadOnlyList<IStepRule> stepRules;
        private readonly IReadOnlyList<IHeuristic> heuristics;
        private readonly IInput<int> input;
        private readonly IMessenger messenger;

        public AStarAlgorithmMenuItem(IReadOnlyList<IStepRule> stepRules,
            IReadOnlyList<IHeuristic> heuristics,
            IInput<int> input, 
            IMessenger messenger)
        {
            this.stepRules = stepRules;
            this.heuristics = heuristics;
            this.input = input;
            this.messenger = messenger;
        }

        public void Execute()
        {
            string menu = stepRules.CreateMenuList(1).ToString();
            int index = InputIndex(menu + '\n' + "Choose step rule: ", stepRules.Count);
            var stepRule = stepRules[index];
            menu = heuristics.CreateMenuList(1).ToString();
            index = InputIndex(menu + '\n' + "Choose heuristic function: ", heuristics.Count);
            var heuristic = heuristics[index];
            var factory = new AStarAlgorithmFactory(stepRule, heuristic);
            messenger.SendData(factory, Tokens.Pathfinding);
        }

        private int InputIndex(string menu, int limit)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = input.Input(menu, limit, 1) - 1;
                return index;
            }
        }

        public override string ToString()
        {
            return "A* algorithm";
        }
    }
}
