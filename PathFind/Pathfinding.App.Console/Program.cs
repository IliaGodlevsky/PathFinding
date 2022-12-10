using Autofac;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.MenuItems;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var scope = DI.Container.BeginLifetimeScope())
        {
            var item = scope.Resolve<MainUnitMenuItem>();
            item.Execute();
        }
    }
}