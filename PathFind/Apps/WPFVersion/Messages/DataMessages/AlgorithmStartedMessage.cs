using Algorithm.Base;
using System;

namespace WPFVersion.Messages.DataMessages
{
    internal sealed class AlgorithmStartedMessage
    {
        public TimeSpan DelayTime { get; }

        public PathfindingAlgorithm Algorithm { get; }

        public AlgorithmStartedMessage(PathfindingAlgorithm algorithm, TimeSpan delayTime)
        {
            Algorithm = algorithm;
            DelayTime = delayTime;
        }
    }
}
