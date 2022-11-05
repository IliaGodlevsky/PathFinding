using Algorithm.Base;
using System;

namespace WPFVersion.Messages.DataMessages
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
