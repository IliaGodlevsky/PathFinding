using Common.Interface;
using GraphViewModel.Interfaces;
using System;
using System.Windows.Input;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel
{
    internal class OpacityChangeViewModel : IModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public double ObstacleColorOpacity { get; set; }
        public double VisitedVertexColorOpacity { get; set; }
        public double EnqueuedVertexColorOpacity { get; set; }
        public double PathVertexColorOpacity { get; set; }
        public double SimpleVertexColorOpacity { get; set; }

        public ICommand ConfirmOpacityChange { get; }
        public ICommand CancelOpacityChange { get; }

        public OpacityChangeViewModel()
        {
            ObstacleColorOpacity = Vertex3D.ObstacleVertexBrush.Opacity;
            VisitedVertexColorOpacity = Vertex3D.VisitedVertexBrush.Opacity;
            EnqueuedVertexColorOpacity = Vertex3D.EnqueuedVertexBrush.Opacity;
            PathVertexColorOpacity = Vertex3D.PathVertexBrush.Opacity;
            SimpleVertexColorOpacity = Vertex3D.SimpleVertexBrush.Opacity;

            ConfirmOpacityChange = new RelayCommand(ExecuteChangeVertexOpacity);
            CancelOpacityChange = new RelayCommand(ExecuteCloseChangeVertexOpacity);
        }

        private void ExecuteChangeVertexOpacity(object param)
        {
            Vertex3D.EnqueuedVertexBrush.Opacity = EnqueuedVertexColorOpacity;
            Vertex3D.ObstacleVertexBrush.Opacity = ObstacleColorOpacity;
            Vertex3D.SimpleVertexBrush.Opacity = SimpleVertexColorOpacity;
            Vertex3D.PathVertexBrush.Opacity = PathVertexColorOpacity;
            Vertex3D.VisitedVertexBrush.Opacity = VisitedVertexColorOpacity;

            OnWindowClosed?.Invoke(this, new EventArgs());
        }

        private void ExecuteCloseChangeVertexOpacity(object param)
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
        }
    }
}
