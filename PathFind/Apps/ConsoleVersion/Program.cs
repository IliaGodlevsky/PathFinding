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
            var mainView = container.Resolve<MainView>();
            mainView.Start();
        }
    }
}