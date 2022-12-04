using Pathfinding.App.Console.Interface;
using Shared.Primitives.Single;
using System;

namespace Pathfinding.App.Console.Model.PathfindingActions
{
    internal sealed class NullAnimationAction 
        : Singleton<NullAnimationAction, IAnimationSpeedAction>, IAnimationSpeedAction
    {
        private NullAnimationAction()
        {

        }

        public TimeSpan Do(TimeSpan current)
        {
            return current;
        }
    }
}
