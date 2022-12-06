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
        private readonly IMessenger messenger;

        public IInput<int> IntInput { get; set; }

        private IDisplayable MenuList { get; }

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
            this.messenger = messenger;
        }

        [MenuItem(MenuItemsNames.ChooseAlgorithm, 0)]
        private void ChoosePathfindingAlgorithm()
        {
            string message = MenuList + "\n" + MessagesTexts.AlgorithmChoiceMsg;
            int index = GetAlgorithmIndex(message);
            while (index != Factories.Count)
            {
                var factory = Factories[index];
                messenger.Send(new PathfindingAlgorithmChosenMessage(factory));
                index = GetAlgorithmIndex(message);
            }
        }

        private int GetAlgorithmIndex(string message)
        {
            using (Cursor.CleanUpAfter())
            {
                return IntInput.Input(message, Factories.Count + 1, 1) - 1;
            }
        }
    }
}