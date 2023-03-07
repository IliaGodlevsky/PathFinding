using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.App.Console.Interface;
using System;
using System.Collections.Generic;

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
            var paramType = typeof(IReadOnlyDictionary<ConsoleKey, IVertexAction>);
            var actions = context.ResolveWithMetadataKeyed<ConsoleKey, IVertexAction>(key);
            var actionsParameter = new TypedParameter(paramType, actions);
            context.ChangeParameters(new[] { actionsParameter });
            next(context);
        }
    }
}
