﻿using Autofac;
using Autofac.Core.Resolving.Pipeline;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class KeyResolveMiddlware<TKey, TValue>(string key) : IResolveMiddleware
    {
        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var instances = context.ResolveWithMetadataKeyed<TKey, TValue>(key);
            var type = typeof(IReadOnlyDictionary<TKey, TValue>);
            var param = new TypedParameter(type, instances);
            context.ChangeParametres(param);
            next(context);
        }
    }
}
