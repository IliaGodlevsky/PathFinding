using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal interface IParametresFactory
    {
        IEnumerable<Parameter> GetParameters(IComponentContext context, Type key);
    }
}
