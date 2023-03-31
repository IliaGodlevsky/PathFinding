using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.DependencyInjection.Registrations;
using Pathfinding.App.Console.MenuItems;

Registry.Configure(
    Registries.Initial,
    Registries.UserInput,
    Registries.PathfindingStatistics,
    Registries.TransitVertices,
    Registries.PathfindingHistory,
    Registries.VisualizationControl,
    Registries.ColorEditor,
    Registries.GraphEditor,
    Registries.WaveAlgorithms,
    Registries.GreedyAlgorithms,
    Registries.BreadthAlgorithms,
    Registries.PathfindingVisualization)
    .Run<MainUnitMenuItem>();