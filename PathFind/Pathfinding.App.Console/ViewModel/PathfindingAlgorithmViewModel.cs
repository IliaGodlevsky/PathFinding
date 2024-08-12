using Pathfinding.App.Console.Model;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Service.Interface;
using ReactiveUI;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel
{
    public class PathfindingAlgorithmViewModel : ReactiveObject
    {
        public IReadOnlyDictionary<string, IAlgorithmFactory<PathfindingProcess>> Algorithms { get; }

        public IReadOnlyDictionary<string, IStepRule> StepRules { get; }

        public IReadOnlyDictionary<string, IHeuristic> Heuristics { get; }

        public ReactiveCommand<Unit, Unit> FindPath { get; }

        public PathfindingAlgorithmViewModel(
            IEnumerable<Pair<string, IAlgorithmFactory<PathfindingProcess>>> algorithms,
            IEnumerable<Pair<string, IStepRule>> stepRules,
            IEnumerable<Pair<string, IHeuristic>> heurtics)
        {
            Algorithms = algorithms.ToDictionary(x => x.Key, x => x.Value).AsReadOnly();
            StepRules = stepRules.ToDictionary(x => x.Key, x => x.Value).AsReadOnly();
            Heuristics = heurtics.ToDictionary(x => x.Key, x => x.Value).AsReadOnly();
        }
    }
}
