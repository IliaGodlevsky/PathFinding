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
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]    
    internal sealed class PathfindingProcessChooseViewModel : ViewModel, IRequireIntInput
    {
        private readonly IMessenger messenger;
        private readonly InclusiveValueRange<int> algorithmKeysValueRange;

        public IInput<int> IntInput { get; set; }

        public string AlgorithmKeyInputMessage { private get; set; }

        private int AlgorithmIndex => IntInput.Input(AlgorithmKeyInputMessage, algorithmKeysValueRange) - 1;

        public ReadOnlyList<IAlgorithmFactory<PathfindingProcess>> Factories { get; }

        public PathfindingProcessChooseViewModel(IEnumerable<IAlgorithmFactory<PathfindingProcess>> factories, 
            IMessenger messenger)
        {
            Factories = factories
                .GroupBy(item => item.GetAttributeOrNull<GroupAttribute>())
                .SelectMany(item => item.OrderByOrderAttribute())
                .ToReadOnly();
            this.messenger = messenger;
            algorithmKeysValueRange = new InclusiveValueRange<int>(Factories.Count, 1);
        }

        [MenuItem(MenuItemsNames.ChooseAlgorithm, 0)]
        private void ChoosePathfindingAlgorithm()
        {
            var algorithm = Factories[AlgorithmIndex];
            messenger.Send(new PathfindingAlgorithmChosenMessage(algorithm));
        }
    }
}