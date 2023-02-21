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
    using Command = IPathfindingRangeCommand<Vertex>;

    internal sealed class RangeBuilderResolveMiddlewear : IResolveMiddleware
    {
        private readonly string metadataKey;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public RangeBuilderResolveMiddlewear(string metadataKey)
        {
            this.metadataKey = metadataKey;
        }

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            context.ChangeParameters(GetParameters(context));
            next(context);
        }

        private IReadOnlyCollection<Command> ResolveKeyed(IComponentContext context, int key)
        {
            return context.ResolveKeyed<IEnumerable<Meta<Command>>>(key)
                .OrderBy(x => x.Metadata[metadataKey])
                .Select(x => x.Value)
                .ToReadOnly();
        }

        private IEnumerable<NamedParameter> GetParameters(IComponentContext context)
        {
            yield return new("includeCommands", ResolveKeyed(context, IncludeCommand));
            yield return new("excludeCommands", ResolveKeyed(context, ExcludeCommand));
        }
    }
}
