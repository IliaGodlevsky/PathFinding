using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.App.Console.DAL;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Shared.Random;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [LowPriority]
    internal sealed class RandomAlgorithmMenuItem(
        IMessenger messenger,
        IRandom random) : AlgorithmMenuItem(messenger)
    {
        private readonly IRandom random = random;

        protected override string LanguageKey => AlgorithmNames.Random;

        protected override AlgorithmInfo GetAlgorithm()
        {
            return new(new RandomAlgorithmFactory(random), RunStatistics);
        }
    }
}
