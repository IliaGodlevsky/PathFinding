using Pathfinding.App.WPF._3D.Interface;
using System;

namespace Pathfinding.App.WPF._3D.Infrastructure.EventArguments
{
    internal class RotationSpeedChangedEventArgs : EventArgs
    {
        public IAnimationSpeed Speed { get; }

        public RotationSpeedChangedEventArgs(IAnimationSpeed speed)
        {
            Speed = speed;
        }
    }
}
