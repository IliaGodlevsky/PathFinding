using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;
using static Pathfinding.App.Console.DependencyInjection.PathfindingUnits;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class UnitConfigurationMiddleware : IResolveMiddleware
    {
        private readonly IReadOnlyDictionary<Type, IUnitMiddleware> middlewares;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public UnitConfigurationMiddleware()
        {
            middlewares = AllUnits.Except(Visual)
                .ToDictionary(unit => unit, unit => (IUnitMiddleware)new UnitMiddleware())
                .Append(new (Visual, new PathfindingVisualizationUnitMiddleware(new UnitMiddleware())))
                .ToReadOnly();
        }

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var key = (Type)context.Registration.Metadata[UnitTypeKey];
            var parametres = middlewares[key].GetParameters(context, key);
            context.ChangeParameters(parametres);
            next(context);
        }
    }
}
