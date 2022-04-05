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
            set => SetAxis(ref xAxis, value); 
        }

        public GraphFieldAxisRotatingViewModel YAxisRotationViewModel 
        { 
            get => yAxis; 
            set => SetAxis(ref yAxis, value); 
        }

        public GraphFieldAxisRotatingViewModel ZAxisRotationViewModel 
        { 
            get => zAxis; 
            set => SetAxis(ref zAxis, value); 
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

        private void SetAxis(ref GraphFieldAxisRotatingViewModel axis, GraphFieldAxisRotatingViewModel value)
        {
            axis = value; 
            axis.RotationSpeed = speed;
            SpeedChanged += axis.OnRotationSpeedChanged;
        }
    }
}
