using Autofac;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Infrastructure;
using WPFVersion.View.Windows;

namespace WPFVersion.ViewModel.ButtonViewModels
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
