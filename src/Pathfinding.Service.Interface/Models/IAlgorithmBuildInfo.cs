using Pathfinding.Domain.Core;

namespace Pathfinding.Service.Interface.Models
{
    public interface IAlgorithmBuildInfo
    {
        Algorithms Algorithm { get; }

        HeuristicFunctions? Heuristics { get; }

        double? Weight { get; }

        StepRules? StepRule { get; }
    }
}
