using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Visualizations;
using Shared.Extensions;
using System;
using System.Collections.Generic;

using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class TotalVisualizationMiddleware : IResolveMiddleware
    {
        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var resolved = context.ResolveWithMetadata<VisualizedType, IVisualizedVertices>(VisualizedTypeKey);
            foreach (var outerVisual in resolved.Values)
            {
                var except = resolved.Values.Except(outerVisual);
                foreach (var innerVisual in except)
                {
                    outerVisual.VertexVisualized += innerVisual.Remove;
                }
            }
            var paramType = typeof(IReadOnlyDictionary<VisualizedType, IVisualizedVertices>);
            var parameter = new TypedParameter(paramType, resolved);
            context.ChangeParametres(parameter);
            next(context);
        }
    }
}
