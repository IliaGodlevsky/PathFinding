using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class UnitConfigurationMiddlewear : IResolveMiddleware
    {
        private readonly IReadOnlyDictionary<Type, UnitMiddlewear> middlewares;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public UnitConfigurationMiddlewear()
        {
            var all = PathfindingUnits.AllUnits.Except(PathfindingUnits.Visual)
                .ToDictionary(unit => unit, unit => new UnitMiddlewear());
            middlewares = new Dictionary<Type, UnitMiddlewear>(all)
            {
                { PathfindingUnits.Visual, new PathfindingVisualizationUnitMiddlewear() }
            };
        }

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var key = (Type)context.Registration.Metadata[UnitTypeKey];
            var middleware = middlewares[key];
            middleware.Execute(context, next, key);
            next(context);
        }
    }
}
