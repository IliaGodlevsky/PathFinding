using System.Windows.Media;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel.VertexOpacityViewModels
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
