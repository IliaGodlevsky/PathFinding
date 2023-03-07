using Autofac.Core.Resolving.Pipeline;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using static Pathfinding.App.Console.DependencyInjection.PathfindingUnits;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class UnitResolveMiddleware : IResolveMiddleware
    {
        private readonly string metadataKey;
        private readonly IReadOnlyDictionary<Type, IUnitMiddleware> middlewares;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public UnitResolveMiddleware(string metadataKey)
        {
            this.metadataKey = metadataKey;
            middlewares = WithoutVisual
                .ToDictionary(unit => unit, unit => (IUnitMiddleware)new UnitMiddleware())
                .Append(new(Visual, new VisualizationUnitResolveMiddleware(new UnitMiddleware())))
                .ToDictionary();
        }

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var key = (Type)context.Registration.Metadata[metadataKey];
            var parametres = middlewares[key].GetParameters(context, key);
            context.ChangeParameters(parametres);
            next(context);
        }
    }
}
