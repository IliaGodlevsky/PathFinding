using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
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

        protected override (IAlgorithmFactory<PathfindingProcess> Algorithm, Statistics Statistics) GetAlgorithm()
        {
            var statistics = new Statistics(nameof(Languages.LeeAlgorithm))
            {
                ResultStatus = nameof(Languages.Started)
            };
            return (new LeeAlgorithmFactory(), statistics);
        }
    }
}
