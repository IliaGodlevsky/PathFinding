using Autofac;
using Autofac.Core;
using Pathfinding.App.Console.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class VisualizationUnitParametresFactory : UnitParamtresFactory
    {
        public override IEnumerable<Parameter> GetParameters(IComponentContext context, Type key)
        {
            return base.GetParameters(context, key).Concat(GetParameters(context));
        }

        private static IReadOnlyCollection<(string, TValue)> Resolve<TValue>(IComponentContext context)
        {
            return context.ResolveWithMetadata<string, TValue>(Key)
                .Select(item => (item.Key, item.Value))
                .ToHashSet();
        }

        private static TypedParameter GetParameter<TKey>(object value)
        {
            return new(typeof(IReadOnlyCollection<(string, TKey)>), value);
        }

        private static IEnumerable<TypedParameter> GetParameters(IComponentContext context)
        {
            yield return GetParameter<IPathfindingAction>(Resolve<IPathfindingAction>(context));
            yield return GetParameter<IAnimationSpeedAction>(Resolve<IAnimationSpeedAction>(context));
        }
    }
}