using Autofac;
using Pathfinding.App.Console.MenuItems;
using System;
using System.Text;

namespace Pathfinding.App.Console.DependencyInjection.Registrations
{
    internal sealed partial class Application : IDisposable
    {
        private readonly ContainerBuilder builder;
        private readonly Lazy<ILifetimeScope> scope;

        private ILifetimeScope Scope => scope.Value;

        public Application()
        {
            builder = new ContainerBuilder();
            scope = new(() => builder.Build());
        }

        public void ApplyFeatures()
        {
            foreach (var feature in GetFeatures())
            {
                feature.Apply(builder);
            }
        }

        public void Run(Encoding outputEncoding)
        {
            Terminal.OutputEncoding = outputEncoding;
            var main = Scope.Resolve<MainUnitMenuItem>();
            main.Execute();
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}