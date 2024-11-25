using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IRequireHeuristicsViewModel
    {
        HeuristicFunctions? Heuristic { get; set; }

        double? Weight { get; set; }
    }
}
