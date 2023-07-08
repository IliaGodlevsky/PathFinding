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
        private readonly IReadOnlyDictionary<Type, IParametresFactory> middlewares;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public UnitResolveMiddleware(string metadataKey, IParametresFactory middleware, params Type[] types)
        {
            this.metadataKey = metadataKey;
            middlewares = types.ToDictionary(unit => unit, unit => middleware).AsReadOnly();
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
