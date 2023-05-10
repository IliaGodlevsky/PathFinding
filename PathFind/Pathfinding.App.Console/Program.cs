using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.DependencyInjection.Registrations;
using Pathfinding.App.Console.MenuItems;
using Pathfinding.App.Console.Settings;
using Shared.Primitives;

using (Disposable.Use(Colours.Default.Save))
{
    Registry
        .Configure(Registries.AppliedRegistries)
        .Run<MainUnitMenuItem>();
}