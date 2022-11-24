using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Shared.Collections;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.ViewModel
{
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;

    [MenuColumnsNumber(1)]
    internal sealed class PathfindingProcessChooseViewModel : ViewModel, IRequireIntInput
    {
        private const int MenuOffset = 6;

        private readonly IMessenger messenger;

        public IInput<int> IntInput { get; set; }

        private IDisplayable MenuList { get; }

        private string InputMessage { get; }

        private int QuitIndex { get; }

        public ReadOnlyList<AlgorithmFactory> Factories { get; }

        public PathfindingProcessChooseViewModel(IEnumerable<AlgorithmFactory> factories, IMessenger messenger)
        {
            Factories = factories
                .GroupBy(item => item.GetAttributeOrDefault<GroupAttribute>())
                .SelectMany(item => item.OrderByOrderAttribute<AlgorithmFactory, OrderAttribute>())
                .ToReadOnly();
            MenuList = Factories.Select(item => item.ToString())
                .Append("Quit")
                .CreateMenuList(columnsNumber: 1);
            InputMessage = MenuList + "\n" + MessagesTexts.AlgorithmChoiceMsg;
            QuitIndex = Factories.Count;
            this.messenger = messenger;
        }

        [MenuItem(MenuItemsNames.ChooseAlgorithm, 0)]
        private void ChoosePathfindingAlgorithm()
        {
            int index = GetAlgorithmIndex(InputMessage);
            while (index != QuitIndex)
            {
                var factory = Factories[index];
                messenger.Send(new PathfindingAlgorithmChosenMessage(factory));
                index = GetAlgorithmIndex(InputMessage);
            }
        }

        private int GetAlgorithmIndex(string message)
        {
            using (Cursor.ClearUpAfter())
            {
                return IntInput.Input(message, Factories.Count + 1, 1) - 1;
            }
        }
    }
}