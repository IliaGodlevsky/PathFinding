using Autofac;
using AutoMapper.Internal;
using Pathfinding.App.Console.MenuItems;
using System;
using System.Text;

namespace Pathfinding.App.Console.DependencyInjection.Registrations
{
    internal sealed partial class Application : IDisposable
    {
        private readonly ContainerBuilder builder = new();
        private readonly Lazy<ILifetimeScope> scope;

        private ILifetimeScope Scope => scope.Value;

        public Application(string title = Constants.Title)
        {
            Terminal.Title = title;
            scope = new(() => builder.Build());
        }

        public void ApplyComponents()
        {
            Components.ForAll(x => x.Apply(builder));
        }

        public void Run(Encoding outputEncoding)
        {
            Terminal.OutputEncoding = outputEncoding;
            Scope.Resolve<MainUnitMenuItem>().Execute();
        }

        public void Run() => Run(Encoding.UTF8);

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}