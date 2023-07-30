using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.App.Console.Model.Notes;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [HighPriority]
    internal sealed class AStarLeeAlgorithmMenuItem : AlgorithmInputMenuItem
    {
        private readonly IReadOnlyDictionary<Heuristics, IHeuristic> heuristics;

        public AStarLeeAlgorithmMenuItem(IReadOnlyDictionary<Heuristics, IHeuristic> heuristics,
            IMessenger messenger,
            IInput<int> intInput) 
            : base(messenger, intInput)
        {
            this.heuristics = heuristics;
        }

        public override string ToString()
        {
            return Languages.AStarLeeAlgorithm;
        }

        protected override (IAlgorithmFactory<PathfindingProcess> Algorithm, Statistics Statistics) GetAlgorithm()
        {
            var heuristic = InputItem(heuristics, Languages.ChooseHeuristicMsg);
            var statistics = new Statistics
            {
                Algorithm = Algorithms.AStarLeeAlgorithm,
                ResultStatus = AlgorithmStatus.Started,
                Heuristics = heuristic.Key
            };
            var factory = new AStarLeeAlgorithmFactory(heuristic.Value);
            return (factory, statistics);
        }
    }
}
