using Pathfinding.Service.Interface;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal interface IRequireHeuristicsViewModel
    {
        public (string Name, IHeuristic Heuristic) Heuristic { get; set; }

        public double Weight { get; set; }
    }
}
