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
            var resolved = context.ResolveWithMetadata<VisualType, IVisual>(VisualTypeKey);
            foreach (var outerVisual in resolved.Values)
            {
                var except = resolved.Values.Except(outerVisual);
                foreach (var innerVisual in except)
                {
                    outerVisual.Visualized += innerVisual.Remove;
                }
            }
            var paramType = typeof(IReadOnlyDictionary<VisualType, IVisual>);
            var parameter = new TypedParameter(paramType, resolved);
            context.ChangeParameters(new[] { parameter });
            next(context);
        }
    }
}
