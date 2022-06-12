using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.PassValueMessages;

namespace WPFVersion3D.ViewModel
{
    internal class GraphFieldAxesRotationViewModel
    {
        private readonly IMessenger messenger;

        private List<GraphFieldAxisRotatingViewModel> AxisRotationViewModels { get; }

        public ICommand RotateCommand { get; }

        public GraphFieldAxesRotationViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            AxisRotationViewModels = new List<GraphFieldAxisRotatingViewModel>();
            RotateCommand = new RelayCommand(ExecuteRotateCommand, CanExecuteRotateCommand);
            messenger.Register<AddRotationViewModelMessage>(this, AddRotationViewModel);
            messenger.Register<RemoveRotationViewModelMessage>(this, RemoveRotationViewModel);
        }

        private void ExecuteRotateCommand(object param)
        {
            AxisRotationViewModels.ForEach(model => model.RotateFieldCommand.Execute(param));
        }

        private bool CanExecuteRotateCommand(object param)
        {
            return AxisRotationViewModels.Count > 0;
        }

        private void AddRotationViewModel(AddRotationViewModelMessage message)
        {
            AxisRotationViewModels.Add(message.Value);
        }

        private void RemoveRotationViewModel(RemoveRotationViewModelMessage message)
        {
            AxisRotationViewModels.Remove(message.Value);
        }

    }
}
