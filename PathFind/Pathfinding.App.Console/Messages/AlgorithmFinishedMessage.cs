using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class AlgorithmFinishedMessage : IHistoryMessage
    {
        public PathfindingProcess Algorithm { get; }

        public string Statistics { get; }

        public AlgorithmFinishedMessage(PathfindingProcess algorithm, string statistics)
        {
            Algorithm = algorithm;
            Statistics = statistics;
        }        
    }
}