//using Autofac;
//using Pathfinding.App.Console;
//using Pathfinding.App.Console.DependencyInjection;
//using Pathfinding.App.Console.MenuItems;
//using System.Text;
//using System.Threading.Tasks;

//internal static class Application
//{
//    public static async Task Run()
//    {
//        Terminal.Title = Constants.Title;
//        Terminal.OutputEncoding = Encoding.UTF8;
//        var builder = new ContainerBuilder();
//        using var container = Build(builder);
//        var mainItem = container.Resolve<MainUnitMenuItem>();
//        await mainItem.ExecuteAsync();
//    }

//    private static IContainer Build(ContainerBuilder builder)
//    {
//        return builder
//            .AddDataAccessLayer()
//            .AddAlgorithms()
//            .AddTransitVertices()
//            .AddColorsEditor()
//            .AddGraphEditor()
//            .AddKeysEditor()
//            .AddGraphSharing()
//            .AddPathfindingControl()
//            .AddPathfindingHistory()
//            .AddPathfindingVisualization()
//            .BuildApplication();
//    }
//}