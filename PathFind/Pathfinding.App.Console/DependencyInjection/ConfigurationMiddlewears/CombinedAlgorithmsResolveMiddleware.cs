using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.AlgorithmLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class CombinedAlgorithmsResolveMiddleware(string key) : IResolveMiddleware
    {
        private readonly string key = key;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var heuristics = context.ResolveWithMetadataKeyed<string, IHeuristic>(key);
            var paramTypeHeuristics = typeof(IReadOnlyDictionary<string, IHeuristic>);
            var heuristicsParam = new TypedParameter(paramTypeHeuristics, heuristics);
            var stepRules = context.ResolveWithMetadataKeyed<string, IStepRule>(key);
            var paramTypeStepRules = typeof(IReadOnlyDictionary<string, IStepRule>);
            var stepRulesParam = new TypedParameter(paramTypeStepRules, stepRules);
            context.ChangeParametres(heuristicsParam, stepRulesParam);
            next(context);
        }
    }
}
