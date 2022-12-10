using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;

    internal sealed class PathfindingAlgorithmMenuItem : IMenuItem
    {
        private readonly IReadOnlyList<AlgorithmFactory> factories;
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private readonly IInput<int> input;

        private IDisplayable MenuList { get; }

        public int Order => 1;

        public PathfindingAlgorithmMenuItem(IEnumerable<AlgorithmFactory> factories, 
            IMessenger messenger, IPathfindingRangeBuilder<Vertex> rangeBuilder, IInput<int> input)
        {
            this.messenger = messenger;
            this.rangeBuilder = rangeBuilder;
            this.input = input;
            this.factories = factories
                .GroupBy(item => item.GetAttributeOrDefault<GroupAttribute>())
                .SelectMany(item => item.OrderByOrderAttribute())
                .ToReadOnly();
            MenuList = this.factories.Select(item => item.ToString())
                .Append("Quit")
                .CreateMenuList();
        }

        public bool CanBeExecuted()
        {
            return rangeBuilder.Range.HasSourceAndTargetSet()
                && !rangeBuilder.Range.HasIsolators();
        }

        public void Execute()
        {
            string message = MenuList + "\n" + MessagesTexts.AlgorithmChoiceMsg;
            int index = GetAlgorithmIndex(message);
            while (index != factories.Count)
            {               
                var factory = factories[index];
                messenger.Send(new PathfindingAlgorithmChosenMessage(factory));          
                index = GetAlgorithmIndex(message);
            }
        }

        private int GetAlgorithmIndex(string message)
        {
            Screen.SetCursorPositionUnderMenu(1);
            using (Cursor.CleanUpAfter())
            {
                return input.Input(message, factories.Count + 1, 1) - 1;
            }
        }

        public override string ToString() => "Find path";
    }
}