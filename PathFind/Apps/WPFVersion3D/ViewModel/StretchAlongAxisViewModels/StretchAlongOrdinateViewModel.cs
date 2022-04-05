using WPFVersion3D.Interface;

namespace WPFVersion3D.ViewModel.StretchAlongAxisViewModels
{
    internal sealed class StretchAlongOrdinateViewModel : BaseStretchAlongAxisViewModel
    {
        protected override IAxis Axis => graphField?.Ordinate;
    }
}
