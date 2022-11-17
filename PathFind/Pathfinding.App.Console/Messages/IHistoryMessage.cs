using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.Console.Messages
{
    internal interface IHistoryMessage
    {
        PathfindingProcess Algorithm { get; }
    }
}
