using System;
using System.Collections.Generic;
using WPFVersion3D.Infrastructure.EventArguments;
using WPFVersion3D.Infrastructure.EventHandlers;
using WPFVersion3D.Interface;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel
{
    internal class GraphFieldRotationViewModel : IDisposable
    {
        public event RotationSpeedChangedEventHandler SpeedChanged;

        private IAnimationSpeed speed;
        private GraphFieldAxisRotatingViewModel xAxis;
        private GraphFieldAxisRotatingViewModel yAxis;
        private GraphFieldAxisRotatingViewModel zAxis;

        public IAnimationSpeed SelectedRotationSpeed
        {
            get => speed;
            set { speed = value; SpeedChanged?.Invoke(this, new RotationSpeedChangedEventArgs(speed)); }
        }

        public GraphFieldAxisRotatingViewModel XAxisRotationViewModel { get => xAxis; set => Set(ref xAxis, value); }

        public GraphFieldAxisRotatingViewModel YAxisRotationViewModel { get => yAxis; set => Set(ref yAxis, value); }

        public GraphFieldAxisRotatingViewModel ZAxisRotationViewModel { get => zAxis; set => Set(ref zAxis, value); }

        public IReadOnlyCollection<IAnimationSpeed> Speeds => AnimationSpeeds.Speeds;

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
