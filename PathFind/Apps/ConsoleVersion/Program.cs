using Autofac;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.View;

namespace ConsoleVersion
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                var view = scope.Resolve<MainView>();
                view.Start();
            }
        }
    }
}