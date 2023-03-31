using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.DependencyInjection.Registrations;
using Pathfinding.App.Console.MenuItems;

Registry.Configure(
    Registries.Initial,
    Registries.UserInput,
    Registries.PathfindingStatistics,
    Registries.TransitVertices,
    Registries.PathfindingHistory,
    Registries.PathfindingControl,
    Registries.ColorEditor,
    Registries.GraphEditor,
    Registries.AllAlgorithms,
    Registries.PathfindingVisualization,
    Registries.SerializerDecorators)
    .Run<MainUnitMenuItem>();