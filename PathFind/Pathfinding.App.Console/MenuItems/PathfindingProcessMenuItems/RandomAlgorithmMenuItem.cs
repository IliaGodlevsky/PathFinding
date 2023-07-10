using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Shared.Random;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [MediumPriority]
    internal sealed class RandomAlgorithmMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IRandom random;

        public RandomAlgorithmMenuItem(IMessenger messenger, IRandom random)
        {
            this.messenger = messenger;
            this.random = random;
        }

        public void Execute()
        {
            var factory = new RandomAlgorithmFactory(random);
            messenger.SendData(factory, Tokens.Pathfinding);
        }

        public override string ToString()
        {
            return "Random algorithm";
        }
    }
}
