using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Infrastructure.EventArguments;
using WPFVersion3D.Interface;
using WPFVersion3D.Messages.PassValueMessages;

namespace WPFVersion3D.ViewModel
{
    internal class GraphFieldAxisRotatingViewModel
    {
        private readonly IMessenger messenger;

        private IAnimationSpeed RotationSpeed { get; set; }

        public AxisAngleRotation3D AngleRotation { get; set; }

        public bool IsEnabled { get; set; } = true;

        public string Title { get; set; }

        public ICommand EnableRotationCommand { get; }

        public ICommand RotateFieldCommand { get; }

        public GraphFieldAxisRotatingViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            RotateFieldCommand = new RelayCommand(ExecuteRotateFieldCommand, CanExecuteRotateFieldCommand);
            EnableRotationCommand = new RelayCommand(ExecuteEnableRotationCommand);
        }

        public void OnRotationSpeedChanged(object sender, RotationSpeedChangedEventArgs e)
        {
            RotationSpeed = e.Speed;
        }

        private void ExecuteEnableRotationCommand(object param)
        {
            if (param is false)
            {
                messenger.Send(new AddRotationViewModelMessage(this));
            }
            else if (param is true)
            {
                messenger.Send(new RemoveRotationViewModelMessage(this));
            }
        }

        private void ExecuteRotateFieldCommand(object parameter)
        {
            var factory = (IAnimatedAxisRotatorFactory)parameter;
            var rotator = factory.Create(RotationSpeed);
            rotator.RotateAxis(AngleRotation);
        }

        private bool CanExecuteRotateFieldCommand(object parameter)
        {
            return IsEnabled;
        }
    }
}