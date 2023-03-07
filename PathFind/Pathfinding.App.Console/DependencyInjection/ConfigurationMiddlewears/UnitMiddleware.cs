using Autofac;
using Autofac.Core;
using Pathfinding.App.Console.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class UnitMiddleware : IUnitMiddleware
    {
        public IEnumerable<Parameter> GetParameters(IComponentContext context, Type key)
        {
            yield return GetParameter<IMenuItem>(Resolve<IMenuItem>(context, key));
            yield return GetParameter<IConditionedMenuItem>(Resolve<IConditionedMenuItem>(context, key));
        }

        private static IReadOnlyCollection<TValue> Resolve<TValue>(IComponentContext context, Type key)
        {
            return context.ResolveKeyed<IReadOnlyCollection<TValue>>(key).ToArray();
        }

        private static TypedParameter GetParameter<TValue>(object value)
        {
            return new(typeof(IReadOnlyCollection<TValue>), value);
        }
    }
}