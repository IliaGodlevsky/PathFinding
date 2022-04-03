using System.Windows.Media;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel.VertexOpacityViewModels
{
    internal class RegularVertexOpacityViewModel : BaseVertexOpacityViewModel
    {
        protected override Brush Color => VertexVisualization.RegularVertexBrush;
    }
}
