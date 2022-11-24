using Pathfinding.App.WPF._3D.Interface;
using System.Windows.Media;

namespace Pathfinding.App.WPF._3D.ViewModel.BaseViewModel
{
    internal abstract class BaseVertexOpacityViewModel : IChangeColorOpacity
    {
        protected abstract Brush Color { get; }

        public string Title { get; set; }

        public double Opacity { get; set; }

        public virtual void ChangeOpacity()
        {
            Color.Opacity = Opacity;
        }

        protected BaseVertexOpacityViewModel()
        {
            Opacity = Color.Opacity;
        }
    }
}