using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Localization;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Random;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class AssembleGraphMenuItem : GraphCreatingMenuItem
    {
        public override int Order => 1;

        public AssembleGraphMenuItem(IMessenger messenger, IRandom random, 
            IVertexCostFactory costFactory, INeighborhoodFactory neighborhoodFactory)
            : base(messenger, random, costFactory, neighborhoodFactory)
        {
            
        }

        public override string ToString()
        {
            return Languages.AssembleGraph;
        }
    }
}
