using Autofac;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.View;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class FindPathViewModel
    {
        private readonly BaseEndPoints endPoints;

        public ICommand FindPathCommand { get; }

        public FindPathViewModel()
        {
            endPoints = DI.Container.Resolve<BaseEndPoints>();
            FindPathCommand = new RelayCommand(ExecuteFindPathCommand, CanExecuteFindPathCommand);
        }

        private void ExecuteFindPathCommand(object param)
        {
            DI.Container.Resolve<PathFindWindow>().Show();
        }

        private bool CanExecuteFindPathCommand(object param)
        {
            return !endPoints.HasIsolators();
        }
    }
}