using Pathfinding.App.WPF._3D.Infrastructure.EventArguments;
using Pathfinding.App.WPF._3D.Infrastructure.EventHandlers;
using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.Model;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.WPF._3D.ViewModel
{
    internal class GraphFieldRotationViewModel : IDisposable
    {
        public event RotationSpeedChangedEventHandler SpeedChanged;

        private IAnimationSpeed speed;
        private GraphFieldAxisRotatingViewModel xAxis;
        private GraphFieldAxisRotatingViewModel yAxis;
        private GraphFieldAxisRotatingViewModel zAxis;

        public IEnumerable<IAnimationSpeed> Speeds => AnimationSpeeds.Speeds;

        public IAnimationSpeed SelectedRotationSpeed
        {
            get => speed;
            set { speed = value; SpeedChanged?.Invoke(this, new RotationSpeedChangedEventArgs(speed)); }
        }

        public GraphFieldAxisRotatingViewModel XAxisRotationViewModel
        {
            get => xAxis;
            set => Set(ref xAxis, value);
        }

        public GraphFieldAxisRotatingViewModel YAxisRotationViewModel
        {
            get => yAxis;
            set => Set(ref yAxis, value);
        }

        public GraphFieldAxisRotatingViewModel ZAxisRotationViewModel
        {
            get => zAxis;
            set => Set(ref zAxis, value);
        }

        public void Dispose()
        {
            SpeedChanged = null;
        }

        private void Set(ref GraphFieldAxisRotatingViewModel axis, GraphFieldAxisRotatingViewModel value)
        {
            axis = value;
            SpeedChanged += axis.OnRotationSpeedChanged;
            axis.OnRotationSpeedChanged(this, new RotationSpeedChangedEventArgs(speed));
        }
    }
}
