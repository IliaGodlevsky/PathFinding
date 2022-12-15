using Autofac;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.MenuItems;
using System.Globalization;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
        using (var scope = Registry.Container.BeginLifetimeScope())
        {
            var item = scope.Resolve<MainUnitMenuItem>();
            item.Execute();
        }
    }
}