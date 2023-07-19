using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.App.Console.Model.Notes;
using Shared.Random;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [MediumPriority]
    internal sealed class RandomAlgorithmMenuItem : AlgorithmMenuItem
    {
        private readonly IRandom random;

        public RandomAlgorithmMenuItem(IMessenger messenger, IRandom random)
            : base(messenger)
        {
            this.random = random;
        }

        public override string ToString()
        {
            return Languages.RandomAlgorithm;
        }

        protected override (IAlgorithmFactory<PathfindingProcess> Algorithm, Statistics Statistics) GetAlgorithm()
        {
            var statistics = new Statistics
            {
                Algorithm = Algorithms.RandomAlgorithm,
                ResultStatus = AlgorithmResultStatus.Started
            };
            return (new RandomAlgorithmFactory(random), statistics);
        }
    }
}
