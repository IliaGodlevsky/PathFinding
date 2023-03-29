using Autofac;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.DependencyInjection.Registrations;
using Pathfinding.App.Console.MenuItems;

internal class Program
{
    private static void Main(string[] args)
    {
        var container = Registry.Configure(Registries.FullRegistration);
        using (var scope = container.BeginLifetimeScope())
        {
            var item = scope.Resolve<MainUnitMenuItem>();
            item.Execute();
        }
    }
}