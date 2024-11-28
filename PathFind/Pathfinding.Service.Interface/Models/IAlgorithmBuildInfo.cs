using Pathfinding.Domain.Core;

namespace Pathfinding.Service.Interface.Models
{
    public interface IAlgorithmBuildInfo
    {
        HeuristicFunctions? Heuristics { get; }

        double? Weight { get; }

        StepRules? StepRule { get; }
    }
}
