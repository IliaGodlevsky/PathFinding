using Autofac;
using Autofac.Core;
using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal class UnitParamtresFactory : IParametresFactory
    {
        public virtual IEnumerable<Parameter> GetParameters(IComponentContext context, Type key)
        {
            yield return GetParameter<IMenuItem>(Resolve<IMenuItem>(context, key));
        }

        private static IReadOnlyCollection<TValue> Resolve<TValue>(IComponentContext context, Type key)
        {
            return context.ResolveKeyed<IReadOnlyCollection<TValue>>(key).ToReadOnly();
        }

        private static TypedParameter GetParameter<TValue>(object value)
        {
            return new(typeof(IReadOnlyCollection<TValue>), value);
        }
    }
}