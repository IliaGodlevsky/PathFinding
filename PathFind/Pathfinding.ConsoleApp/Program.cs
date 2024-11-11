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
        using var scope = App.Build();
        var main = scope.Resolve<MainView>();
        Application.Top.Add(main);
        Application.Run();
    }
}