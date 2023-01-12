using Autofac;
using Autofac.Core;
using Pathfinding.App.Console.Interface;
using Shared.Collections;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class UnitMiddleware : IUnitMiddleware
    {
        public IEnumerable<Parameter> GetParameters(IComponentContext context, Type key)
        {
            yield return GetParameter<IMenuItem>(Resolve<IMenuItem>(context, key));
            yield return GetParameter<IConditionedMenuItem>(Resolve<IConditionedMenuItem>(context, key));
        }

        private static ReadOnlyList<TValue> Resolve<TValue>(IComponentContext context, Type key)
        {
            return context.ResolveKeyed<IReadOnlyCollection<TValue>>(key).ToReadOnly();
        }

        private static TypedParameter GetParameter<TValue>(object value)
        {
            return new TypedParameter(typeof(IReadOnlyCollection<TValue>), value);
        }
    }
}