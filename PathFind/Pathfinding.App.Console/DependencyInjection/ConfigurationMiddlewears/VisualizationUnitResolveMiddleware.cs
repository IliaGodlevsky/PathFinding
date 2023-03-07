﻿using Autofac;
using Autofac.Core;
using Pathfinding.App.Console.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class VisualizationUnitResolveMiddleware : IUnitMiddleware
    {
        private readonly IUnitMiddleware middleware;

        public VisualizationUnitResolveMiddleware(IUnitMiddleware middleware)
        {
            this.middleware = middleware;
        }

        public IEnumerable<Parameter> GetParameters(IComponentContext context, Type key)
        {
            return middleware.GetParameters(context, key).Concat(GetParameters(context));
        }

        private static IReadOnlyDictionary<ConsoleKey, TValue> Resolve<TValue>(IComponentContext context)
        {
            return context.ResolveWithMetadata<ConsoleKey, TValue>(Key);
        }

        private static TypedParameter GetParameter<TKey>(object value)
        {
            return new(typeof(IReadOnlyDictionary<ConsoleKey, TKey>), value);
        }

        private static IEnumerable<TypedParameter> GetParameters(IComponentContext context)
        {
            yield return GetParameter<IPathfindingAction>(Resolve<IPathfindingAction>(context));
            yield return GetParameter<IAnimationSpeedAction>(Resolve<IAnimationSpeedAction>(context));
        }
    }
}