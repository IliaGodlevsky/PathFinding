using Autofac;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems;
using Pathfinding.App.Console.Units;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (key.Equals(typeof(PathfindingVisualizationUnit)))
            {
                var pathfindingActions = context.ResolveWithMetadata<ConsoleKey, IPathfindingAction>(Key);
                var animationActions = context.ResolveWithMetadata<ConsoleKey, IAnimationSpeedAction>(Key);
                var pathfindingParam = new TypedParameter(typeof(IReadOnlyDictionary<ConsoleKey, IPathfindingAction>), pathfindingActions);
                var animationParam = new TypedParameter(typeof(IReadOnlyDictionary<ConsoleKey, IAnimationSpeedAction>), animationActions);
                parametres.AddRange(pathfindingParam, animationParam);
            }
            var menuItems = context.ResolveKeyed<IReadOnlyCollection<IMenuItem>>(key).ToList();
            if (!key.Equals(typeof(MainUnit)))
            {
                var exitMenuItem = context.Resolve<ExitMenuItem>();
                menuItems.Add(exitMenuItem);
            }
            var menuItemsParam = new TypedParameter(typeof(IReadOnlyCollection<IMenuItem>), menuItems);
            var conditioned = context.ResolveKeyed<IReadOnlyCollection<IConditionedMenuItem>>(key);
            var conditionedParams = new TypedParameter(typeof(IReadOnlyCollection<IConditionedMenuItem>), conditioned);
            parametres.AddRange(menuItemsParam, conditionedParams);
            context.ChangeParameters(parametres);
            next(context);
        }
    }
}
