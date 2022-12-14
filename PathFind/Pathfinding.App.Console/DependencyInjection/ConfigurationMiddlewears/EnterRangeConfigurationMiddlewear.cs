using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.App.Console.Interface;
using System;
using System.Collections.Generic;

using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class EnterRangeConfigurationMiddlewear : IResolveMiddleware
    {
        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {           
            var actions = context.ResolveWithMetadata<ConsoleKey, IVertexAction>(Key);
            var actionsParameter = new TypedParameter(typeof(IReadOnlyDictionary<ConsoleKey, IVertexAction>), actions);
            context.ChangeParameters(new[] { actionsParameter });
            next(context);
        }
    }
}
