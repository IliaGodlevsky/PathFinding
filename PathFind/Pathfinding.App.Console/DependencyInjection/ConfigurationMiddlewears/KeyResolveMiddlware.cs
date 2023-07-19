using Autofac;
using Autofac.Core.Resolving.Pipeline;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class KeyResolveMiddlware<TKey, TValue> : IResolveMiddleware
    {
        private readonly string key;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public KeyResolveMiddlware(string key)
        {
            this.key = key;
        }

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var heuristics = context.ResolveWithMetadataKeyed<TKey, TValue>(key);
            var type = typeof(IReadOnlyDictionary<TKey, TValue>);
            var param = new TypedParameter(type, heuristics);
            context.ChangeParametres(param);
            next(context);
        }
    }
}
