using Autofac;
using Autofac.Core;
using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class PathfindingVisualizationUnitMiddlewear : UnitMiddlewear
    {
        protected override IList<Parameter> GetParameters(IComponentContext context, Type key)
        {
            var parametres = new List<Parameter>();
            var pathfindingActions = context.ResolveWithMetadata<ConsoleKey, IPathfindingAction>(Key).ToReadOnly();
            var animationActions = context.ResolveWithMetadata<ConsoleKey, IAnimationSpeedAction>(Key).ToReadOnly();
            var pathfindingParam = new TypedParameter(typeof(IReadOnlyDictionary<ConsoleKey, IPathfindingAction>), pathfindingActions);
            var animationParam = new TypedParameter(typeof(IReadOnlyDictionary<ConsoleKey, IAnimationSpeedAction>), animationActions);
            parametres.AddRange(pathfindingParam, animationParam);
            parametres.AddRange(base.GetParameters(context, key));
            return parametres;
        }
    }
}
