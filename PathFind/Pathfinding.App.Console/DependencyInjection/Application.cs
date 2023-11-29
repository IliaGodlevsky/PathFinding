﻿using Autofac;
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

        public void ApplyComponents()
        {
            foreach (var component in GetComponents())
            {
                component.Apply(builder);
            }
        }

        public void Run(Encoding outputEncoding)
        {
            Terminal.OutputEncoding = outputEncoding;
            Scope.Resolve<MainUnitMenuItem>().Execute();
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}