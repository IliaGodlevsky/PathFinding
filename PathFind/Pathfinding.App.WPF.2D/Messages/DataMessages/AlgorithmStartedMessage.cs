using Pathfinding.AlgorithmLib.Core.Abstractions;
using System;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class AlgorithmStartedMessage
    {
        public TimeSpan DelayTime { get; }

        public PathfindingProcess Algorithm { get; }

        public AlgorithmStartedMessage(PathfindingProcess algorithm, TimeSpan delayTime)
        {
            Algorithm = algorithm;
            DelayTime = delayTime;
        }
    }
}
