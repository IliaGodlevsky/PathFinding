using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class AlgorithmStartInfoMessage
    {
        public IAlgorithmFactory<PathfindingProcess> Factory { get; }

        public RunStatisticsDto InitStatistics { get; }

        public AlgorithmStartInfoMessage(IAlgorithmFactory<PathfindingProcess> factory,
            RunStatisticsDto initStatistics)
        {
            Factory = factory;
            InitStatistics = initStatistics;
        }
    }
}
