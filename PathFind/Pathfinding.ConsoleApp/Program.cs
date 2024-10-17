using Autofac;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.View;
using Terminal.Gui;

namespace System.Runtime.CompilerServices
{
    internal record IsExternalInit;
}

internal class Program
{
    private static void Main()
    {
        Application.Init();
        using var container = Container.CreateBuilder()
            .WithAlgorithms()
            .WithImportExport()
            .WithDatabase()
            .WithLogging()
            .WithTransitVertices()
            .BuildApp();
        var mainView = container.Resolve<MainView>();
        Application.Top.Add(mainView);
        Application.Run(x => true);
    }
}