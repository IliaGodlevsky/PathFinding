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
            ObstacleColorOpacity = Wpf3dVertex.ObstacleVertexBrush.Opacity;
            VisitedVertexColorOpacity = Wpf3dVertex.VisitedVertexBrush.Opacity;
            EnqueuedVertexColorOpacity = Wpf3dVertex.EnqueuedVertexBrush.Opacity;
            PathVertexColorOpacity = Wpf3dVertex.PathVertexBrush.Opacity;
            SimpleVertexColorOpacity = Wpf3dVertex.SimpleVertexBrush.Opacity;

            Model = model as MainWindowViewModel;
            ConfirmOpacityChange = new RelayCommand(ExecuteChangeVertexOpacity, obj => true);
            CancelOpacityChange = new RelayCommand(ExecuteCloseChangeVertexOpacity, obj => true);
        }

        private void ExecuteChangeVertexOpacity(object param)
        {
            Wpf3dVertex.EnqueuedVertexBrush.Opacity = EnqueuedVertexColorOpacity;
            Wpf3dVertex.ObstacleVertexBrush.Opacity = ObstacleColorOpacity;
            Wpf3dVertex.SimpleVertexBrush.Opacity = SimpleVertexColorOpacity;
            Wpf3dVertex.PathVertexBrush.Opacity = PathVertexColorOpacity;
            Wpf3dVertex.VisitedVertexBrush.Opacity = VisitedVertexColorOpacity;

            Model?.Window?.Close();
        }

        private void ExecuteCloseChangeVertexOpacity(object param)
        {
            Model?.Window?.Close();
        }
    }
}
