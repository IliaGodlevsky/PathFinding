using System.Windows.Input;
using System.Windows.Media.Media3D;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Infrastructure.EventArguments;
using WPFVersion3D.Interface;

namespace WPFVersion3D.ViewModel
{
    internal class GraphFieldAxisRotatingViewModel
    {
        private IAnimationSpeed RotationSpeed { get; set; }

        public AxisAngleRotation3D AngleRotation { get; set; }

        public string Title { get; set; }

        public ICommand RotateFieldCommand { get; }

        public GraphFieldAxisRotatingViewModel()
        {
            RotateFieldCommand = new RelayCommand(ExecuteRotateFieldCommand);
        }

        public void OnRotationSpeedChanged(object sender, RotationSpeedChangedEventArgs e)
        {
            RotationSpeed = e.Speed;
        }

        private void ExecuteRotateFieldCommand(object parameter)
        {
            var factory = (IAnimatedAxisRotatorFactory)parameter;
            var rotator = factory.Create(RotationSpeed);
            rotator.RotateAxis(AngleRotation);
        }
    }
}