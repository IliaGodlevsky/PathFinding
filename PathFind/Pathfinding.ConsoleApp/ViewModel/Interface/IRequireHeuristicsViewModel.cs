using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IRequireHeuristicsViewModel
    {
        HeuristicFunctions? Heuristic { get; set; }

        double? FromWeight { get; set; }

        double? ToWeight { get; set; }

        double? Step { get; set; }
    }
}
