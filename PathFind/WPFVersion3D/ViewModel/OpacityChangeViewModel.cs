using Common.Interfaces;
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

        public MainWindowViewModel Model { get; set; }

        public OpacityChangeViewModel(IMainModel model)
        {
            ObstacleColorOpacity = WpfVertex3D.ObstacleVertexBrush.Opacity;
            VisitedVertexColorOpacity = WpfVertex3D.VisitedVertexBrush.Opacity;
            EnqueuedVertexColorOpacity = WpfVertex3D.EnqueuedVertexBrush.Opacity;
            PathVertexColorOpacity = WpfVertex3D.PathVertexBrush.Opacity;
            SimpleVertexColorOpacity = WpfVertex3D.SimpleVertexBrush.Opacity;

            Model = model as MainWindowViewModel;
            ConfirmOpacityChange = new RelayCommand(ExecuteChangeVertexOpacity);
            CancelOpacityChange = new RelayCommand(ExecuteCloseChangeVertexOpacity);
        }

        private void ExecuteChangeVertexOpacity(object param)
        {
            WpfVertex3D.EnqueuedVertexBrush.Opacity = EnqueuedVertexColorOpacity;
            WpfVertex3D.ObstacleVertexBrush.Opacity = ObstacleColorOpacity;
            WpfVertex3D.SimpleVertexBrush.Opacity = SimpleVertexColorOpacity;
            WpfVertex3D.PathVertexBrush.Opacity = PathVertexColorOpacity;
            WpfVertex3D.VisitedVertexBrush.Opacity = VisitedVertexColorOpacity;

            OnWindowClosed?.Invoke(this, new EventArgs());
        }

        private void ExecuteCloseChangeVertexOpacity(object param)
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
        }
    }
}
