using Autofac;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.View;
using Terminal.Gui;

internal class Program
{
    private static void Main(string[] args)
    {
        using var container = Container.BuildApp();
        Application.Init();
        var mainView = container.Resolve<MainView>();
        Application.Top.Add(mainView);
        Application.Run(x => true);
    }
}