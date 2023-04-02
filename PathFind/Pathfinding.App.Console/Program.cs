using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.DependencyInjection.Registrations;
using Pathfinding.App.Console.MenuItems;

Registry.Configure(
    Registries.Initial,
    Registries.UserInput,
    Registries.PathfindingStatistics,
    Registries.PathfindingVisualization,
    Registries.TransitVertices,
    Registries.GraphSharing,
    Registries.PathfindingHistory,
    Registries.VisualizationControl,
    Registries.ColorEditor,
    Registries.GraphEditor,
    Registries.WaveAlgorithms,
    Registries.BreadthAlgorithms,
    Registries.GreedyAlgorithms)
    .Run<MainUnitMenuItem>();