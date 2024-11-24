namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IRequireHeuristicsViewModel
    {
        string Heuristic { get; set; }

        double Weight { get; set; }
    }
}
