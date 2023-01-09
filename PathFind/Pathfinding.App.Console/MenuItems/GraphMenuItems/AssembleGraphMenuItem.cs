using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Random;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [HighestPriority]
    internal sealed class AssembleGraphMenuItem : GraphCreatingMenuItem
    {
        public AssembleGraphMenuItem(IMessenger messenger,
            IRandom random,
            IVertexCostFactory costFactory,
            INeighborhoodFactory neighborhoodFactory)
            : base(messenger, random, costFactory, neighborhoodFactory)
        {

        }

        public override string ToString()
        {
            return Languages.AssembleGraph;
        }
    }
}
