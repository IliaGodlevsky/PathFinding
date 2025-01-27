using Autofac;
using Pathfinding.App.Console.View;
using Terminal.Gui;

namespace Pathfinding.App.Console
{
    internal class Program
    {
        private static void Main()
        {
            Application.Init();
            using var scope = Injection.App.Build();
            var main = scope.Resolve<MainView>();
            Application.Top.Add(main);
            Application.Run(x => true);
        }
    }
}