using Autofac;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class UnitConfigurationMiddlewear : IResolveMiddleware
    {
        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var parametres = new List<Parameter>();
            var metadata = context.Registration.Metadata;
            var key = metadata[UnitTypeKey];
            if (key.Equals(Visual))
            {
                var pathfindingActions = context.ResolveWithMetadata<ConsoleKey, IPathfindingAction>(Key).ToReadOnly();
                var animationActions = context.ResolveWithMetadata<ConsoleKey, IAnimationSpeedAction>(Key).ToReadOnly();
                var pathfindingParam = new TypedParameter(typeof(IReadOnlyDictionary<ConsoleKey, IPathfindingAction>), pathfindingActions);
                var animationParam = new TypedParameter(typeof(IReadOnlyDictionary<ConsoleKey, IAnimationSpeedAction>), animationActions);
                parametres.AddRange(pathfindingParam, animationParam);
            }
            var menuItems = context.ResolveKeyed<IReadOnlyCollection<IMenuItem>>(key).ToReadOnly();
            var menuItemsParam = new TypedParameter(typeof(IReadOnlyCollection<IMenuItem>), menuItems);
            var conditioned = context.ResolveKeyed<IReadOnlyCollection<IConditionedMenuItem>>(key).ToReadOnly();
            var conditionedParams = new TypedParameter(typeof(IReadOnlyCollection<IConditionedMenuItem>), conditioned);
            parametres.AddRange(menuItemsParam, conditionedParams);
            context.ChangeParameters(parametres);
            next(context);
        }
    }
}
