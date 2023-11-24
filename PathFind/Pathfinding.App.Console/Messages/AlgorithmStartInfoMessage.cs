using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Model.Notes;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class AlgorithmStartInfoMessage
    {
        public IAlgorithmFactory<PathfindingProcess> Factory { get; }

        public Statistics InitStatistics { get; }

        public AlgorithmStartInfoMessage(IAlgorithmFactory<PathfindingProcess> factory,
            Statistics initStatistics)
        {
            Factory = factory;
            InitStatistics = initStatistics;
        }
    }
}
