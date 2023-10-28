using Autofac;

namespace Pathfinding.App.Console.DependencyInjection.Registrations
{
    internal interface IComponent
    {
        void Apply(ContainerBuilder builder);
    }
}
