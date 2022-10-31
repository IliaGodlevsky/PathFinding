using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Realizations.Graphs;
using NullObject.Extensions;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.Model;
using WPFVersion3D.View;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class ChangeOpacityViewModel
    {
        private readonly IMessenger messenger;

        private Graph3D<Vertex3D> Graph { get; set; } = Graph3D<Vertex3D>.Empty;

        public ICommand ChangeVerticesOpacityCommand { get; }

        public ChangeOpacityViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            ChangeVerticesOpacityCommand = new RelayCommand(ExecuteChangeOpacityCommand, CanExecuteChangeOpacityCommand);
        }

        private void ExecuteChangeOpacityCommand(object param)
        {
            DI.Container.Resolve<OpacityChangeWindow>().Show();
        }

        private bool CanExecuteChangeOpacityCommand(object param)
        {
            return !Graph.IsNull();
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Value;
        }
    }
}
