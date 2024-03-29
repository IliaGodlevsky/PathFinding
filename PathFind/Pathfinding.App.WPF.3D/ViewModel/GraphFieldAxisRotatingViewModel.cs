﻿using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Infrastructure.EventArguments;
using Pathfinding.App.WPF._3D.Infrastructure.States;
using Pathfinding.App.WPF._3D.Interface;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.ViewModel
{
    internal class GraphFieldAxisRotatingViewModel
    {
        public string Title { get; set; }

        public AxisAngleRotation3D AngleRotation { get; set; }

        public ICommand EnableRotationCommand { get; }

        public ICommand RotateFieldCommand { get; }

        private IRotationState RotationState { get; set; }

        private IAnimationSpeed RotationSpeed { get; set; }

        public GraphFieldAxisRotatingViewModel()
        {
            RotationState = new EnabledRotationState();
            RotateFieldCommand = new RelayCommand(ExecuteRotateFieldCommand, CanExecuteRotateFieldCommand);
            EnableRotationCommand = new RelayCommand(ExecuteEnableRotationCommand);
        }

        public void OnRotationSpeedChanged(object sender, RotationSpeedChangedEventArgs e)
        {
            RotationSpeed = e.Speed;
        }

        private void ExecuteEnableRotationCommand(object param)
        {
            if (param is IRotationState state)
            {
                RotationState = state;
                RotationState.Activate(this);
            }
        }

        private void ExecuteRotateFieldCommand(object parameter)
        {
            if (parameter is IAnimatedAxisRotatorFactory factory)
            {
                var rotator = factory.Create(RotationSpeed);
                rotator.RotateAxis(AngleRotation);
            }
        }

        private bool CanExecuteRotateFieldCommand(object parameter)
        {
            return RotationState.CanRotate;
        }
    }
}