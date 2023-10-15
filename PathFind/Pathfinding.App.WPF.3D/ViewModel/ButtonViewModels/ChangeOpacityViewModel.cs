using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.App.WPF._3D.View;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.ViewModel.ButtonViewModels
{
    internal class ChangeOpacityViewModel
    {
        private readonly IMessenger messenger;

        private IGraph<Vertex3D> Graph { get; set; } = Graph<Vertex3D>.Empty;

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
            return Graph != Graph<Vertex3D>.Empty;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Value;
        }
    }
}
