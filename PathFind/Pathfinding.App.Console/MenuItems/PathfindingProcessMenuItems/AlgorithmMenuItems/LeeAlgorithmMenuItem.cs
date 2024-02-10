using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.App.Console.Model.Notes;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [MediumPriority]
    internal sealed class LeeAlgorithmMenuItem : AlgorithmMenuItem
    {
        public LeeAlgorithmMenuItem(IMessenger messenger)
            : base(messenger)
        {
        }

        public override string ToString()
        {
            return Languages.LeeAlgorithm;
        }

        protected override AlgorithmInfo GetAlgorithm()
        {
            var statistics = new RunStatisticsDto 
            { 
                AlgorithmId = nameof(Languages.LeeAlgorithm) 
            };
            return new(new LeeAlgorithmFactory(), statistics);
        }
    }
}
