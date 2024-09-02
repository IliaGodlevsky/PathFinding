using Autofac;
using Pathfinding.ConsoleApp.View;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.Injection
{
    internal static class MainApp
    {
        public static void Run(string[] args)
        {
            using (var container = ConfigureApplication())
            {
                Application.Init();
                Colors.Base.Normal
                    = Application.Driver.MakeAttribute(Color.White, Color.Black);
                var mainView = container.Resolve<MainView>();
                Application.Top.Add(mainView);
                Application.Run();
            }
        }

        private static ILifetimeScope ConfigureApplication()
        {
            return new ContainerBuilder().BuildApp();
        }
    }
}
