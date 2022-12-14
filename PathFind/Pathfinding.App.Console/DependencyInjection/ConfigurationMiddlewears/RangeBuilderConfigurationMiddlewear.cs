using Autofac;
using Autofac.Core;
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
            var parametres = new List<Parameter>();
            var include = ResolveKeyed(context, IncludeCommand);
            var exclude = ResolveKeyed(context, ExcludeCommand);
            var includeParametre = new NamedParameter("includeCommands", include);
            var excludeParametre = new NamedParameter("excludeCommands", exclude);
            parametres.AddRange(includeParametre, excludeParametre);
            context.ChangeParameters(parametres);
            next(context);
        }

        private IReadOnlyCollection<IPathfindingRangeCommand<Vertex>> ResolveKeyed(IComponentContext context, int key)
        {
            return context.ResolveKeyed<IEnumerable<Meta<IPathfindingRangeCommand<Vertex>>>>(key)
                .OrderBy(x => x.Metadata[Order])
                .Select(x => x.Value)
                .ToReadOnly();
        }
    }
}
