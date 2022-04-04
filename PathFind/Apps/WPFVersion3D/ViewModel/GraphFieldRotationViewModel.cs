using EnumerationValues.Realizations;
using System;
using System.Collections.Generic;
using WPFVersion3D.Enums;
using WPFVersion3D.Extensions;
using WPFVersion3D.Infrastructure.EventArguments;
using WPFVersion3D.Infrastructure.EventHandlers;
using WPFVersion3D.Interface;

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

        public GraphFieldAxisRotatingViewModel XAxisRotationViewModel 
        {
            get => xAxis;
            set { xAxis = value; xAxis.RotationSpeed = speed; SpeedChanged += xAxis.OnRotationSpeedChanged; }
        }

        public GraphFieldAxisRotatingViewModel YAxisRotationViewModel 
        {
            get => yAxis;
            set { yAxis = value; yAxis.RotationSpeed = speed; SpeedChanged += yAxis.OnRotationSpeedChanged; }
        }

        public GraphFieldAxisRotatingViewModel ZAxisRotationViewModel 
        {
            get => zAxis;
            set { zAxis = value; zAxis.RotationSpeed = speed; SpeedChanged += zAxis.OnRotationSpeedChanged; }
        }

        public IReadOnlyCollection<Tuple<string, IAnimationSpeed>> AnimationSpeeds { get; }

        public GraphFieldRotationViewModel()
        {
            AnimationSpeeds = new EnumValuesWithoutIgnored<AnimationSpeeds>().ToAnimationSpeedTuples();
        }

        public void Dispose()
        {
            SpeedChanged = null;
        }
    }
}
