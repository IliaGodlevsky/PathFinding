using System.Windows.Media;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel.VertexOpacityViewModels
{
    internal class EnqueuedVertexOpacityViewModel : BaseVertexOpacityViewModel
    {
        protected override Brush Color => VertexVisualization.EnqueuedVertexBrush;
    }
}