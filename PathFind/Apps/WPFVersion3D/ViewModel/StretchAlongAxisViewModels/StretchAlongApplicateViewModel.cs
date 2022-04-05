using WPFVersion3D.Interface;

namespace WPFVersion3D.ViewModel.StretchAlongAxisViewModels
{
    internal sealed class StretchAlongApplicateViewModel : BaseStretchAlongAxisViewModel
    {
        protected override IAxis Axis => graphField?.Applicate;
    }
}
