using Autofac;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal class UnitMiddlewear
    {
        public virtual void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next, Type key)
        {
            var parametres = GetParameters(context, key);
            context.ChangeParameters(parametres);
        }

        protected virtual IList<Parameter> GetParameters(IComponentContext context, Type key)
        {
            var parametres = new List<Parameter>();
            var menuItems = context.ResolveKeyed<IReadOnlyCollection<IMenuItem>>(key).ToReadOnly();
            var menuItemsParam = new TypedParameter(typeof(IReadOnlyCollection<IMenuItem>), menuItems);
            var conditioned = context.ResolveKeyed<IReadOnlyCollection<IConditionedMenuItem>>(key).ToReadOnly();
            var conditionedParams = new TypedParameter(typeof(IReadOnlyCollection<IConditionedMenuItem>), conditioned);
            parametres.AddRange(menuItemsParam, conditionedParams);
            return parametres;
        }
    }
}
