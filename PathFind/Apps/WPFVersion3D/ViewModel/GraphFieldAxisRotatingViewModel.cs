using System.Windows.Input;
using System.Windows.Media.Media3D;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Infrastructure.EventArguments;
using WPFVersion3D.Infrastructure.States;
using WPFVersion3D.Interface;

namespace WPFVersion3D.ViewModel
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
            RotationState = (IRotationState)param;
            RotationState.Activate(this);
        }

        private void ExecuteRotateFieldCommand(object parameter)
        {
            var factory = (IAnimatedAxisRotatorFactory)parameter;
            var rotator = factory.Create(RotationSpeed);
            rotator.RotateAxis(AngleRotation);
        }

        private bool CanExecuteRotateFieldCommand(object parameter)
        {
            return RotationState.CanRotate;
        }
    }
}