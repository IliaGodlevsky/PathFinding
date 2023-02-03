using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.ViewModel.BaseViewModel;

namespace Pathfinding.App.WPF._3D.ViewModel.StretchAlongAxisViewModels
{
    internal sealed class StretchAlongAbscissaViewModel : BaseStretchAlongAxisViewModel
    {
        protected override IAxis Axis => graphField.Abscissa;
    }
}
