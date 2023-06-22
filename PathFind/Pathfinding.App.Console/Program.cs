using Autofac;
using Pathfinding.App.Console.DependencyInjection.Registrations;
using Pathfinding.App.Console.MenuItems;

public class Program
{
    private static void Main(string[] args)
    {
        using (var container = Registry.Configure())
        {
            var main = container.Resolve<MainUnitMenuItem>();
            main.Execute();
        }
    }
}