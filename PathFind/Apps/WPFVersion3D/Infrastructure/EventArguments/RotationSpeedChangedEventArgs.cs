using System;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.EventArguments
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
