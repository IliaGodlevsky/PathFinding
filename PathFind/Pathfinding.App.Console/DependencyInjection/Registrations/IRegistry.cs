using Autofac;

namespace Pathfinding.App.Console.DependencyInjection.Registrations
{
    internal interface IRegistry
    {
        void Configure(ContainerBuilder builder);
    }
}
