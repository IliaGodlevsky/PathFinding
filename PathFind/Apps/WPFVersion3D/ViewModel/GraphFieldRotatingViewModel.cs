using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Infrastructure.Animators;
using WPFVersion3D.Interface;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel
{
    internal class GraphFieldRotatingViewModel : IDisposable
    {
        private readonly Dictionary<AxisRotators, Func<IAnimationSpeed, IAnimatedAxisRotator>> rotationFactories;

        private readonly IMessenger messenger;      
        private IAnimationSpeed rotationSpeed;

        public AxisAngleRotation3D AngleRotation { get; set; }

        public string Title { get; set; }

        public ICommand RotateFieldCommand { get; }

        public GraphFieldRotatingViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<AnimationSpeedChangedMessage>(this, MessageTokens.GraphFieldRotationModel, OnAnimationSpeedChanged);
            RotateFieldCommand = new RelayCommand(ExecuteRotateFieldCommand);
            rotationFactories = new Dictionary<AxisRotators, Func<IAnimationSpeed, IAnimatedAxisRotator>>
            {
                { AxisRotators.ForwardRotator,  speed => new ForwardAnimatedAxisRotator(speed) },
                { AxisRotators.BackwardRotator, speed => new BackwardAnimatedAxisRotator(speed) },
                { AxisRotators.None, speed => NullAnimatedAxisRotator.Instance }
            };
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }

        private void ExecuteRotateFieldCommand(object parameter)
        {
            var rotatorType = parameter is AxisRotators value ? value : AxisRotators.None;
            var rotator = rotationFactories[rotatorType].Invoke(rotationSpeed);
            rotator.RotateAxis(AngleRotation);
        }

        private void OnAnimationSpeedChanged(AnimationSpeedChangedMessage message)
        {
            rotationSpeed = message.Value;
        }
    }
}