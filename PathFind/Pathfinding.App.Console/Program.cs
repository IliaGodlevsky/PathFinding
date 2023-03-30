using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.DependencyInjection.Registrations;
using Pathfinding.App.Console.MenuItems;

Registry
    .Configure(Registries.FullRegistration)
    .Start<MainUnitMenuItem>();