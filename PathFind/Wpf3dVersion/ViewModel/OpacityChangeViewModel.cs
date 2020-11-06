using GraphViewModel.Interfaces;
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
            ObstacleColorOpacity = Wpf3dVertex.ObstacleVertexMaterial.Brush.Opacity;
            VisitedVertexColorOpacity = Wpf3dVertex.VisitedVertexMaterial.Brush.Opacity;
            EnqueuedVertexColorOpacity = Wpf3dVertex.EnqueuedVertexMaterial.Brush.Opacity;
            PathVertexColorOpacity = Wpf3dVertex.PathVertexMaterial.Brush.Opacity;
            SimpleVertexColorOpacity = Wpf3dVertex.SimpleVertexMaterial.Brush.Opacity;

            Model = model as MainWindowViewModel;
            ConfirmOpacityChange = new RelayCommand(ExecuteChangeVertexOpacity, obj => true);
            CancelOpacityChange = new RelayCommand(ExecuteCloseChangeVertexOpacity, obj => true);
        }

        private void ExecuteChangeVertexOpacity(object param)
        {
            Wpf3dVertex.EnqueuedVertexMaterial.Brush.Opacity = EnqueuedVertexColorOpacity;
            Wpf3dVertex.ObstacleVertexMaterial.Brush.Opacity = ObstacleColorOpacity;
            Wpf3dVertex.SimpleVertexMaterial.Brush.Opacity = SimpleVertexColorOpacity;
            Wpf3dVertex.PathVertexMaterial.Brush.Opacity = PathVertexColorOpacity;
            Wpf3dVertex.VisitedVertexMaterial.Brush.Opacity = VisitedVertexColorOpacity;

            Model?.Window?.Close();
        }

        private void ExecuteCloseChangeVertexOpacity(object param)
        {
            Model?.Window?.Close();
        }
    }
}
