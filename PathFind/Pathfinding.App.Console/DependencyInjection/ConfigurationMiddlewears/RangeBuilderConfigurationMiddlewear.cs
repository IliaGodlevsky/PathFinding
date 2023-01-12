using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Autofac.Features.Metadata;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class RangeBuilderConfigurationMiddlewear : IResolveMiddleware
    {
        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            context.ChangeParameters(GetParameters(context));
            next(context);
        }

        private static IReadOnlyCollection<IPathfindingRangeCommand<Vertex>> 
            ResolveKeyed(IComponentContext context, int key)
        {
            return context.ResolveKeyed<IEnumerable<Meta<IPathfindingRangeCommand<Vertex>>>>(key)
                .OrderBy(x => x.Metadata[Order])
                .Select(x => x.Value)
                .ToReadOnly();
        }

        private static NamedParameter GetParameter(string name, object value)
        {
            return new NamedParameter(name, value);
        }

        private static IEnumerable<NamedParameter> GetParameters(IComponentContext context)
        {
            yield return GetParameter("includeCommands", ResolveKeyed(context, IncludeCommand));
            yield return GetParameter("excludeCommands", ResolveKeyed(context, ExcludeCommand));
        }
    }
}
