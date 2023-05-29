using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Autofac.Features.Metadata;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;

    internal sealed class PathfindingItemResolveMiddleware : IResolveMiddleware
    {
        private readonly string groupKey;
        private readonly string orderKey;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public PathfindingItemResolveMiddleware(string groupKey, string orderKey)
        {
            this.groupKey = groupKey;
            this.orderKey = orderKey;
        }

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var value = context.Resolve<IEnumerable<Meta<AlgorithmFactory>>>()
                .GroupBy(item => item.Metadata[groupKey])
                .SelectMany(item => item.OrderBy(meta => meta.Metadata[orderKey]))
                .Select(item => item.Value)
                .ToArray();
            var parameter = new TypedParameter(typeof(IReadOnlyList<AlgorithmFactory>), value);
            context.ChangeParametres(parameter);
            next(context);
        }
    }
}
