using Autofac;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Model;
using WPFVersion3D.View;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class FindPathViewModel
    {
        private readonly BaseEndPoints<Vertex3D> endPoints;

        public ICommand FindPathCommand { get; }

        public FindPathViewModel()
        {
            endPoints = DI.Container.Resolve<BaseEndPoints<Vertex3D>>();
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