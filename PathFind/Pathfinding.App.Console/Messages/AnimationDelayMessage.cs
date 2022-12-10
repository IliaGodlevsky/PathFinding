using System;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class AnimationDelayMessage
    {
        public TimeSpan AnimationDelay { get; }

        public AnimationDelayMessage(TimeSpan animationDelay)
        {
            AnimationDelay = animationDelay;
        }
    }
}
