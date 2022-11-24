using Pathfinding.App.WPF._3D.Model;
using Pathfinding.App.WPF._3D.ViewModel.BaseViewModel;
using System.Windows.Media;

namespace Pathfinding.App.WPF._3D.ViewModel.VertexOpacityViewModels
{
    internal class PathVertexOpacityViewModel : BaseVertexOpacityViewModel
    {
        protected override Brush Color => VertexVisualization.PathVertexBrush;

        public override void ChangeOpacity()
        {
            base.ChangeOpacity();
            VertexVisualization.AlreadyPathVertexBrush.Opacity = Opacity;
        }
    }
}
