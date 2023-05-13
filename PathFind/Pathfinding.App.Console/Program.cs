using Autofac;
using Pathfinding.App.Console.DependencyInjection.Registrations;
using Pathfinding.App.Console.MenuItems;

using (var container = Registry.Configure())
{
    var main = container.Resolve<MainUnitMenuItem>();
    main.Execute();
}