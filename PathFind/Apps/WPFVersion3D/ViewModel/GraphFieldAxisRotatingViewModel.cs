using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using WPFVersion3D.Enums;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Infrastructure.Animators;
using WPFVersion3D.Infrastructure.EventArguments;
using WPFVersion3D.Interface;

namespace WPFVersion3D.ViewModel
{
    internal class GraphFieldAxisRotatingViewModel
    {
        private readonly IReadOnlyDictionary<AxisRotators, Func<IAnimationSpeed, IAnimatedAxisRotator>> rotationFactories;

        public IAnimationSpeed RotationSpeed { get; set; }

        public AxisAngleRotation3D AngleRotation { get; set; }

        public string Title { get; set; }

        public ICommand RotateFieldCommand { get; }

        public GraphFieldAxisRotatingViewModel()
        {
            RotateFieldCommand = new RelayCommand(ExecuteRotateFieldCommand);
            rotationFactories = new Dictionary<AxisRotators, Func<IAnimationSpeed, IAnimatedAxisRotator>>
            {
                { AxisRotators.ForwardRotator,  speed => new ForwardAnimatedAxisRotator(speed) },
                { AxisRotators.BackwardRotator, speed => new BackwardAnimatedAxisRotator(speed) },
                { AxisRotators.None, speed => NullAnimatedAxisRotator.Instance }
            };
        }

        public void OnRotationSpeedChanged(object sender, RotationSpeedChangedEventArgs e)
        {
            RotationSpeed = e.Speed;
        }

        private void ExecuteRotateFieldCommand(object parameter)
        {
            var rotatorType = parameter is AxisRotators value ? value : AxisRotators.None;
            var rotator = rotationFactories[rotatorType].Invoke(RotationSpeed);
            rotator.RotateAxis(AngleRotation);
        }
    }
}