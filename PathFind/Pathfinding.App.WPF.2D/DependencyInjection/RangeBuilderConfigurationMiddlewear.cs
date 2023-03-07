using Autofac;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Autofac.Features.Metadata;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

using static Pathfinding.App.WPF._2D.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.WPF._2D.DependencyInjection
{
    internal sealed class RangeBuilderConfigurationMiddlewear : IResolveMiddleware
    {
        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var include = ResolveKeyed(context, IncludeCommand);
            var exclude = ResolveKeyed(context, ExcludeCommand);
            var includeParametre = new NamedParameter("includeCommands", include);
            var excludeParametre = new NamedParameter("excludeCommands", exclude);
            context.ChangeParameters(new[] { includeParametre, excludeParametre });
            next(context);
        }

        private IReadOnlyCollection<IPathfindingRangeCommand<Vertex>> ResolveKeyed(IComponentContext context, int key)
        {
            return context.ResolveKeyed<IEnumerable<Meta<IPathfindingRangeCommand<Vertex>>>>(key)
                .OrderBy(x => x.Metadata[Order])
                .Select(x => x.Value)
                .ToArray();
        }
    }
}
