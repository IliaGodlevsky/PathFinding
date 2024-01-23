using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.App.Console.Interface;
using System;
using System.Collections.Generic;

using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class VerticesVisualizationMiddleware : IResolveMiddleware
    {
        private readonly string metadataKey;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public VerticesVisualizationMiddleware(string metadataKey)
        {
            this.metadataKey = metadataKey;
        }

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var resolved = context.ResolveWithMetadata<string, IVisualizedVertices>(metadataKey);
            var paramType = typeof(IReadOnlyDictionary<string, IVisualizedVertices>);
            var parameter = new TypedParameter(paramType, resolved);
            context.ChangeParametres(parameter);
            next(context);
        }
    }
}
