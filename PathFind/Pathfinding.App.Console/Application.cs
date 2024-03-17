using Autofac;
using Pathfinding.App.Console;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.MenuItems;
using System.Text;

internal static class Application
{
    public static void Run(string[] args)
    {
        Terminal.Title = Constants.Title;
        Terminal.OutputEncoding = Encoding.UTF8;
        var builder = new ContainerBuilder();
        using var container = Build(builder);
        container.Resolve<MainUnitMenuItem>().Execute();
    }

    private static IContainer Build(ContainerBuilder builder)
    {
        return builder
            .AddDataAccessLayer()
            .AddAlgorithms()
            .AddTransitVertices()
            .AddColorsEditor()
            .AddGraphEditor()
            .AddKeysEditor()
            .AddGraphSharing()
            .AddPathfindingControl()
            .AddPathfindingHistory()
            .AddPathfindingStatistics()
            .AddPathfindingVisualization()
            .BuildApplication();
    }
}