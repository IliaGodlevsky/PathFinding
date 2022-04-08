using System.Windows.Media;
using WPFVersion3D.Model;
using WPFVersion3D.ViewModel.BaseViewModel;

namespace WPFVersion3D.ViewModel.VertexOpacityViewModels
{
    internal class VisistedVertexOpacityViewModel : BaseVertexOpacityViewModel
    {
        protected override Brush Color => VertexVisualization.VisitedVertexBrush;
    }
}
