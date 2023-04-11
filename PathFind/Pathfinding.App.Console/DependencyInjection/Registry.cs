using Autofac;
using Pathfinding.App.Console.DependencyInjection.Registrations;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DependencyInjection
{
    internal static class Registry
    {
        public static ILifetimeScope Configure(IEnumerable<IRegistry> registries)
        {
            var builder = new ContainerBuilder();

            foreach (var registry in registries)
            {
                registry.Configure(builder);
            }

            return builder.Build();
        }

        public static ILifetimeScope Configure(params IRegistry[] registries)
        {
            return Configure(registries.AsEnumerable());
        }
    }
}