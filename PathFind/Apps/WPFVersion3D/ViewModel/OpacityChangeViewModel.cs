using Common.Interface;
using System;
using System.Windows.Input;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel
{
    public class OpacityChangeViewModel : IViewModel, IDisposable
    {
        public event Action WindowClosed;

        public double ObstacleColorOpacity { get; set; }
        public double VisitedVertexColorOpacity { get; set; }
        public double EnqueuedVertexColorOpacity { get; set; }
        public double PathVertexColorOpacity { get; set; }
        public double RegularVertexColorOpacity { get; set; }

        public ICommand ConfirmOpacityChange { get; }

        public ICommand CancelOpacityChange { get; }

        public OpacityChangeViewModel()
        {
            ObstacleColorOpacity = VertexVisualization.ObstacleVertexBrush.Opacity;
            VisitedVertexColorOpacity = VertexVisualization.VisitedVertexBrush.Opacity;
            EnqueuedVertexColorOpacity = VertexVisualization.EnqueuedVertexBrush.Opacity;
            PathVertexColorOpacity = VertexVisualization.PathVertexBrush.Opacity;
            RegularVertexColorOpacity = VertexVisualization.RegularVertexBrush.Opacity;

            ConfirmOpacityChange = new RelayCommand(ExecuteChangeVertexOpacity);
            CancelOpacityChange = new RelayCommand(ExecuteCloseChangeVertexOpacity);
        }

        public void Dispose()
        {
            WindowClosed = null;
        }

        private void ExecuteChangeVertexOpacity(object param)
        {
            VertexVisualization.EnqueuedVertexBrush.Opacity = EnqueuedVertexColorOpacity;
            VertexVisualization.ObstacleVertexBrush.Opacity = ObstacleColorOpacity;
            VertexVisualization.RegularVertexBrush.Opacity = RegularVertexColorOpacity;
            VertexVisualization.PathVertexBrush.Opacity = PathVertexColorOpacity;
            VertexVisualization.VisitedVertexBrush.Opacity = VisitedVertexColorOpacity;
            VertexVisualization.AlreadyPathVertexBrush.Opacity = PathVertexColorOpacity;

            ExecuteCloseChangeVertexOpacity(param);
        }

        private void ExecuteCloseChangeVertexOpacity(object param)
        {
            WindowClosed?.Invoke();
        }
    }
}
