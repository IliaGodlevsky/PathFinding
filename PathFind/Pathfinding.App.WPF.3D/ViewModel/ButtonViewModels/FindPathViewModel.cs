using Autofac;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.App.WPF._3D.View;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.ViewModel.ButtonViewModels
{
    internal class FindPathViewModel
    {
        private readonly IPathfindingRangeBuilder<Vertex3D> rangeBuilder;

        public ICommand FindPathCommand { get; }

        public FindPathViewModel()
        {
            rangeBuilder = DI.Container.Resolve<IPathfindingRangeBuilder<Vertex3D>>();
            FindPathCommand = new RelayCommand(ExecuteFindPathCommand, CanExecuteFindPathCommand);
        }

        private void ExecuteFindPathCommand(object param)
        {
            DI.Container.Resolve<PathfindingWindow>().Show();
        }

        private bool CanExecuteFindPathCommand(object param)
        {
            return !rangeBuilder.Range.HasIsolators();
        }
    }
}