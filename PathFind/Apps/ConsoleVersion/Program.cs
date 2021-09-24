using Autofac;
using ConsoleVersion.Configure;
using ConsoleVersion.Interface;

namespace ConsoleVersion
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var container = ContainerConfigure.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var mainView = scope.Resolve<IView>();
                mainView.Start();
            }
        }
    }
}