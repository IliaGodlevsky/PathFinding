using Autofac;
using Autofac.Core.Resolving.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var instances = context.ResolveWithMetadataKeyed<TKey, TValue>(key);
            var type = typeof(IReadOnlyDictionary<TKey, TValue>);
            var param = new TypedParameter(type, instances);
            var parametres = context.Parameters.ToList();
            context.ChangeParametres(param);
            next(context);
        }
    }
}
