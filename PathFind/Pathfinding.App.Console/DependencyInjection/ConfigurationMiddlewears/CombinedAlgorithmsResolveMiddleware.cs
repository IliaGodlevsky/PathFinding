using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.App.Console.Model.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class CombinedAlgorithmsResolveMiddleware : IResolveMiddleware
    {
        private readonly string key;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public CombinedAlgorithmsResolveMiddleware(string key)
        {
            this.key = key;
        }

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
