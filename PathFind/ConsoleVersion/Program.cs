using Autofac;
using ConsoleVersion.App;
using ConsoleVersion.Configure;

namespace ConsoleVersion
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = ContainerConfigure.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run();
            }
        }
    }
}
