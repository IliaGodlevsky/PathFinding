using Common.Interface;
using GraphViewModel.Interfaces;
using System;
using System.Windows.Input;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel
{
    public class OpacityChangeViewModel : IModel, IViewModel
    {
        public event EventHandler WindowClosed;

        public double ObstacleColorOpacity { get; set; }
        public double VisitedVertexColorOpacity { get; set; }
        public double EnqueuedVertexColorOpacity { get; set; }
        public double PathVertexColorOpacity { get; set; }
        public double SimpleVertexColorOpacity { get; set; }

        public ICommand ConfirmOpacityChange { get; }
        public ICommand CancelOpacityChange { get; }

        public OpacityChangeViewModel()
        {
            ObstacleColorOpacity = VerticesColorsHub.ObstacleVertexBrush.Opacity;
            VisitedVertexColorOpacity = VerticesColorsHub.VisitedVertexBrush.Opacity;
            EnqueuedVertexColorOpacity = VerticesColorsHub.EnqueuedVertexBrush.Opacity;
            PathVertexColorOpacity = VerticesColorsHub.PathVertexBrush.Opacity;
            SimpleVertexColorOpacity = VerticesColorsHub.RegularVertexBrush.Opacity;

            ConfirmOpacityChange = new RelayCommand(ExecuteChangeVertexOpacity);
            CancelOpacityChange = new RelayCommand(ExecuteCloseChangeVertexOpacity);
        }

        private void ExecuteChangeVertexOpacity(object param)
        {
            VerticesColorsHub.EnqueuedVertexBrush.Opacity = EnqueuedVertexColorOpacity;
            VerticesColorsHub.ObstacleVertexBrush.Opacity = ObstacleColorOpacity;
            VerticesColorsHub.RegularVertexBrush.Opacity = SimpleVertexColorOpacity;
            VerticesColorsHub.PathVertexBrush.Opacity = PathVertexColorOpacity;
            VerticesColorsHub.VisitedVertexBrush.Opacity = VisitedVertexColorOpacity;
            VerticesColorsHub.AlreadyPathVertexBrush.Opacity = PathVertexColorOpacity;

            ExecuteCloseChangeVertexOpacity(param);
        }

        private void ExecuteCloseChangeVertexOpacity(object param)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
            WindowClosed = null;
        }
    }
}
