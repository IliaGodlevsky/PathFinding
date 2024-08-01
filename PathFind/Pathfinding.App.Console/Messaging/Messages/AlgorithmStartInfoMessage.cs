using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Service.Interface;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class AlgorithmStartInfoMessage
    {
        public IAlgorithmFactory<PathfindingProcess> Factory { get; }

        public string AlgorithmId { get; }

        public string StepRule { get; }

        public string Heuristics { get; }

        public int? Spread { get; }

        public AlgorithmStartInfoMessage(IAlgorithmFactory<PathfindingProcess> factory,
            string algorithmId,
            string stepRule = null,
            string heuristics = null,
            int? spread = null)
        {
            Factory = factory;
            AlgorithmId = algorithmId;
            StepRule = stepRule;
            Heuristics = heuristics;
            Spread = spread;
        }
    }
}
