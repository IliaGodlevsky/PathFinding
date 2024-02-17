using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.App.Console.DAL;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.App.Console.Model.Notes;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [MediumPriority]
    internal sealed class LeeAlgorithmMenuItem(IMessenger messenger) 
        : AlgorithmMenuItem(messenger)
    {
        protected override string LanguageKey => AlgorithmNames.Lee;

        protected override AlgorithmInfo GetAlgorithm()
        {
            return new(new LeeAlgorithmFactory(), RunStatistics);
        }
    }
}
