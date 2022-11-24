using Autofac;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.App.WPF._2D.View;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.Visualization.Core.Abstractions;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel.ButtonViewModels
{
    internal class FindPathViewModel
    {
        private readonly VisualPathfindingRange<Vertex> adapter;

        public ICommand FindPathCommand { get; }

        public FindPathViewModel()
        {
            adapter = DI.Container.Resolve<VisualPathfindingRange<Vertex>>();
            FindPathCommand = new RelayCommand(ExecuteFindPathCommand, CanExecuteFindPathCommand);
        }

        private void ExecuteFindPathCommand(object param)
        {
            DI.Container.Resolve<PathfindingWindow>().Show();
        }

        private bool CanExecuteFindPathCommand(object param)
        {
            return !adapter.HasIsolators() && adapter.HasSourceAndTargetSet();
        }
    }
}
