using Autofac;
using ConsoleVersion.Configure;
using ConsoleVersion.View;

namespace ConsoleVersion
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var container = ContainerConfigure.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var view = scope.Resolve<MainView>();
                view.Start();
            }
        }
    }
}