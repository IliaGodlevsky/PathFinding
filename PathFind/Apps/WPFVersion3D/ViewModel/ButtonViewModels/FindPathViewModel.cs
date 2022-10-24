using Autofac;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.View;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class FindPathViewModel
    {
        private readonly BasePathfindingRange pathfindingRange;

        public ICommand FindPathCommand { get; }

        public FindPathViewModel()
        {
            pathfindingRange = DI.Container.Resolve<BasePathfindingRange>();
            FindPathCommand = new RelayCommand(ExecuteFindPathCommand, CanExecuteFindPathCommand);
        }

        private void ExecuteFindPathCommand(object param)
        {
            DI.Container.Resolve<PathFindWindow>().Show();
        }

        private bool CanExecuteFindPathCommand(object param)
        {
            return !pathfindingRange.HasIsolators();
        }
    }
}