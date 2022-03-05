using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Extensions;
using ConsoleVersion.Views;

namespace ConsoleVersion
{
    internal static class Program
    {
        private static void Main(string[] args) => DI.Container.Display<MainView>();
    }
}