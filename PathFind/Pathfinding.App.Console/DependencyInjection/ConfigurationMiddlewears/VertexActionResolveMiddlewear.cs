using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.App.Console.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class VertexActionResolveMiddlewear : IResolveMiddleware
    {
        private readonly string key;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public VertexActionResolveMiddlewear(string key)
        {
            this.key = key;
        }

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var paramType = typeof(IReadOnlyCollection<(string, IVertexAction)>);
            var actions = context.ResolveWithMetadataKeyed<string, IVertexAction>(key)
                .Select(item => (item.Key, item.Value))
                .ToHashSet();
            var actionsParameter = new TypedParameter(paramType, actions);
            context.ChangeParametres(actionsParameter);
            next(context);
        }
    }
}
