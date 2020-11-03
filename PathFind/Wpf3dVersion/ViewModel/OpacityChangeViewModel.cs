using GraphViewModel.Interfaces;
using System.Linq;
using System.Windows.Media.Media3D;
using Wpf3dVersion.Infrastructure;
using Wpf3dVersion.Model;

namespace Wpf3dVersion.ViewModel
{
    internal class OpacityChangeViewModel : IModel
    {
        public double ObstacleColorOpacity { get; set; }
        public double VisitedVertexColorOpacity { get; set; }
        public double EnqueuedVertexColorOpacity { get; set; }
        public double PathVertexColorOpacity { get; set; }
        public double SimpleVertexColorOpacity { get; set; }

        public RelayCommand ConfirmOpacityChange { get; }
        public RelayCommand CancelOpacityChange { get; }

        public MainWindowViewModel Model { get; set; }
        public OpacityChangeViewModel(IMainModel model)
        {
            ObstacleColorOpacity = (Wpf3dVertex.ObstacleVertexColor.Children.
                           First() as DiffuseMaterial).Brush.Opacity;
            VisitedVertexColorOpacity = (Wpf3dVertex.AfterVisitVertexColor.Children.
                           First() as DiffuseMaterial).Brush.Opacity;
            EnqueuedVertexColorOpacity = (Wpf3dVertex.EnqueuedVertexColor.Children.
                           First() as DiffuseMaterial).Brush.Opacity;
            PathVertexColorOpacity = (Wpf3dVertex.PathVertexColor.Children.
                           First() as DiffuseMaterial).Brush.Opacity;
            SimpleVertexColorOpacity = (Wpf3dVertex.SimpleVertexColor.Children.
                           First() as DiffuseMaterial).Brush.Opacity;
            Model = model as MainWindowViewModel;
            ConfirmOpacityChange = new RelayCommand(ExecuteChangeVertexOpacity, obj => true);
            CancelOpacityChange = new RelayCommand(ExecuteCancelChangeVertexOpacity, obj => true);
        }

        private void ExecuteChangeVertexOpacity(object param)
        {
            (Wpf3dVertex.EnqueuedVertexColor.Children.
                First() as DiffuseMaterial).Brush.Opacity = EnqueuedVertexColorOpacity;
            (Wpf3dVertex.ObstacleVertexColor.Children.
                First() as DiffuseMaterial).Brush.Opacity = ObstacleColorOpacity;
            (Wpf3dVertex.SimpleVertexColor.Children.
                First() as DiffuseMaterial).Brush.Opacity = SimpleVertexColorOpacity;
            (Wpf3dVertex.PathVertexColor.Children.
                First() as DiffuseMaterial).Brush.Opacity = PathVertexColorOpacity;
            (Wpf3dVertex.AfterVisitVertexColor.Children.
                First() as DiffuseMaterial).Brush.Opacity = VisitedVertexColorOpacity;
            Model?.Window?.Close();
        }

        private void ExecuteCancelChangeVertexOpacity(object param)
        {
            Model?.Window?.Close();
        }
    }
}
