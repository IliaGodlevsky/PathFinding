using Autofac;

namespace Pathfinding.App.Console.DependencyInjection.Registrations
{
    internal interface IFeature
    {
        void Apply(ContainerBuilder builder);
    }
}
