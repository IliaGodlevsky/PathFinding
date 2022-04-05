using WPFVersion3D.Interface;

namespace WPFVersion3D.ViewModel.StretchAlongAxisViewModels
{
    internal sealed class StretchAlongAbscissaViewModel : BaseStretchAlongAxisViewModel
    {
        protected override IAxis Axis => graphField?.Abscissa;
    }
}
