using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.App.Console.Model.Notes;
using Shared.Random;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [LowPriority]
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

        protected override AlgorithmInfo GetAlgorithm()
        {
            var statistics = new Statistics(nameof(Languages.RandomAlgorithm));
            return new(new RandomAlgorithmFactory(random), statistics);
        }
    }
}
